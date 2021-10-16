using AutoMapper;
using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.Utilities.Messages;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using MeetingReservationApp.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Concrete
{
    public class RoomReservationManager : IRoomReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomReservationManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<IList<Room>>> GetAvailableRooms(string desiredDate, int startHours, int startMinutes, int endHours, int endMinutes, int locationId)
        {

            var ddate = DateTime.ParseExact(desiredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            #region Create dates
            DateTime desiredStartDate = ddate.Date.AddHours(startHours).AddMinutes(startMinutes);
            DateTime desiredEndDate = ddate.Date.AddHours(endHours).AddMinutes(endMinutes);
            #endregion

            #region Check Time Interval is During Office Hours
            var result = await CheckHoursForLocation(new RoomReservation { MeetingStartTime = desiredStartDate, MeetingEndTime = desiredEndDate },
                                                    locationId);
            if (result.ResultStatus != ResultStatus.Success)
            {
                return new DataResult<IList<Room>>(ResultStatus.Error, result.Message, null);
            }
            #endregion

            List<Room> availableRooms = new List<Room>();
            var offices = await _unitOfWork.Rooms.GetAllAsync(x => x.LocationId == locationId);
            if (offices.Count > 0)
            {
                foreach (var office in offices)
                {
                    result = await CheckHoursForOffice(desiredStartDate, desiredEndDate, office.Id);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        availableRooms.Add(office);
                    }
                }
                if (availableRooms.Count > 0)
                {
                    return new DataResult<List<Room>>(ResultStatus.Success, availableRooms);
                }
            }
            // not any office available
            return new DataResult<List<Room>>(ResultStatus.Error, Messages.RoomReservation.HoursNotAvailableForOffice(), null);
        }
        public async Task<IResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var newReservation = _mapper.Map<RoomReservation>(roomReservationAddDto);

            #region Check Time Interval is During Office Hours
            var result = await CheckHoursForLocation(newReservation, roomReservationAddDto.LocationId);
            if (result.ResultStatus != ResultStatus.Success)
            {
                return result;
            }
            #endregion

            #region Check if Another Meeting Exists During the Time Interval
            result = await CheckHoursForOffice(newReservation.MeetingStartTime, newReservation.MeetingEndTime, newReservation.RoomId);
            if (result.ResultStatus != ResultStatus.Success)
            {
                return result;
            }
            #endregion

            #region Check Attendant Capacity
            result = await CheckAttendantCapacity(newReservation.RoomId, newReservation.AttendantCount);
            if (result.ResultStatus != ResultStatus.Success)
            {
                return result;
            }
            #endregion

            #region Add Reservation For Room
            newReservation.RoomReservationGuid = Guid.NewGuid();
            await _unitOfWork.RoomReservations.AddAsync(newReservation);
            #endregion

            #region Add Reservation For Inventory Related To The Room
            var roomInventories = await _unitOfWork.Inventories.GetAllAsync(x => x.RoomId == newReservation.RoomId);
            foreach (var inventory in roomInventories)
            {
                if (inventory.IsFixed)
                {   // if inventory is fixed, no other reservations could be created on it - directly add it to current reservation
                    await _unitOfWork.InventoryReservations.AddAsync(new InventoryReservation { RoomReservationGuid = newReservation.RoomReservationGuid, InventoryId = inventory.Id });
                }
                else
                {
                    var inventoryReservations = await _unitOfWork.InventoryReservations.GetAllAsync(x => x.InventoryId == inventory.Id &&
                                                                                                   ((newReservation.MeetingStartTime >= x.RoomReservation.MeetingStartTime && newReservation.MeetingStartTime <= x.RoomReservation.MeetingEndTime) ||
                                                                                                   (newReservation.MeetingEndTime >= x.RoomReservation.MeetingStartTime && newReservation.MeetingEndTime <= x.RoomReservation.MeetingEndTime) ||
                                                                                                   (newReservation.MeetingStartTime.Date == x.RoomReservation.MeetingStartTime.Date &&
                                                                                                   newReservation.MeetingStartTime <= x.RoomReservation.MeetingStartTime && newReservation.MeetingEndTime >= x.RoomReservation.MeetingEndTime)),
                                                                                                    x => x.RoomReservation);
                    if (inventoryReservations.Count <= 0)
                    { // if room's inventory is not fixed and have no other reservations - directly add it to current reservation
                        await _unitOfWork.InventoryReservations.AddAsync(new InventoryReservation { RoomReservationGuid = newReservation.RoomReservationGuid, InventoryId = inventory.Id });
                    }
                }

            }
            #endregion

            await _unitOfWork.SaveAsync(); // sava all both reservations and related inventories
            return new Result(ResultStatus.Success, Messages.RoomReservation.Success());
        }

        public async Task<IDataResult<IList<RoomReservation>>> GetAllAsync(int locationId)
        {
            var roomReservations = await _unitOfWork.RoomReservations.GetAllAsync(x => x.Room.LocationId == locationId, x=>x.Room, x=>x.InventoryReservations);
           
            if (roomReservations.Count > -1)
            {
                #region Include Inventories
                foreach (var r in roomReservations)
                {
                    foreach (var i in r.InventoryReservations)
                    {
                        i.Inventory = await _unitOfWork.Inventories.GetAsync(x => x.Id == i.InventoryId);
                    }
                }
                #endregion
                return new DataResult<IList<RoomReservation>>(ResultStatus.Success, roomReservations);
            }
            return new DataResult<IList<RoomReservation>>(ResultStatus.Warning, Messages.RoomReservation.NotFound(),null);
        }


        #region Private Bussiness Methods
        private async Task<IResult> CheckHoursForLocation(RoomReservation newReservation, int locationId)
        {
            #region Get location and create working hours with date
            var location = await _unitOfWork.Locations.GetAsync(x => x.Id == locationId);
            DateTime locationStartDate = newReservation.MeetingStartTime.Date.AddHours(location.OfficeStartHours).AddMinutes(location.OfficeStartMinutes);
            DateTime locationEndDate = newReservation.MeetingStartTime.Date.AddHours(location.OfficeEndHours).AddMinutes(location.OfficeEndMinutes);
            #endregion

            if (!(newReservation.MeetingStartTime >= locationStartDate && newReservation.MeetingStartTime <= locationEndDate &&
                newReservation.MeetingEndTime >= locationStartDate && newReservation.MeetingEndTime <= locationEndDate))
            {   // desired meeting time isn't between office working hours

                return new Result(ResultStatus.Error, Messages.RoomReservation.LocationHoursNotAvailable(location.Name));
            }
            return new Result(ResultStatus.Success);
        }
        private async Task<IResult> CheckHoursForOffice(DateTime newStartTime, DateTime newEndTime, int roomId)
        {
            // check office availability for desired hours
            // example
            // desired time start: 10:00, end : 12:00
            // meeting 1 start: 09:00 end 11:00 => (newStartTime >= x.MeetingStartTime && newStartTime <= x.MeetingEndTime) catches it            
            // meeting 2 start: 11:30 end 12:00 => (newEndTime >= x.MeetingStartTime && newEndTime <= x.MeetingEndTime) catches it
            // metting 3 start: 10:30 end 11:30 =>   newStartTime <= x.MeetingStartTime && newEndTime >= x.MeetingEndTime catches it
            var exists = await _unitOfWork.RoomReservations.AnyAsync(x => x.RoomId == roomId &&
                                                                      ((newStartTime >= x.MeetingStartTime && newStartTime <= x.MeetingEndTime) ||
                                                                       (newEndTime >= x.MeetingStartTime && newEndTime <= x.MeetingEndTime) ||
                                                                       (newStartTime.Date == x.MeetingStartTime.Date &&
                                                                       newStartTime <= x.MeetingStartTime && newEndTime >= x.MeetingEndTime)));

            if (exists)
            {
                // Another meeting exists at the desired time interval
                return new Result(ResultStatus.Error, Messages.RoomReservation.AnotherMeetingExists());
            }
            return new Result(ResultStatus.Success);
        }
        private async Task<IResult> CheckAttendantCapacity(int roomId, int attendantCount)
        {
            var office = await _unitOfWork.Rooms.GetAsync(x => x.Id == roomId);

            if (office.AttendanceCapacity < attendantCount)
            {
                return new Result(ResultStatus.Error, Messages.RoomReservation.AttendantCountExceeds());
            }
            return new Result(ResultStatus.Success);
        }

        #endregion
    }
}
