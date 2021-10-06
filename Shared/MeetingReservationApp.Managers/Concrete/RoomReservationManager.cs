using AutoMapper;
using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using MeetingReservationApp.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IDataResult<IList<Room>>> GetAvailableRooms(RoomAvailabilitySearchDto roomAvailabilitySearchDto, int locationId)
        {
            #region Create dates
            DateTime desiredStartDate = roomAvailabilitySearchDto.DesiredDate.Date.AddHours(roomAvailabilitySearchDto.StartHours).AddMinutes(roomAvailabilitySearchDto.StartMinutes);
            DateTime desiredEndDate = roomAvailabilitySearchDto.DesiredDate.Date.AddHours(roomAvailabilitySearchDto.EndHours).AddMinutes(roomAvailabilitySearchDto.EndMinutes);
            #endregion

            List<Room> availableRooms = new List<Room>();
            var offices = await _unitOfWork.Rooms.GetAllAsync(x => x.LocationId == locationId);
            if (offices != null)
            {
                foreach (var office in offices)
                {
                    var result = await CheckHoursForOffice(desiredStartDate, desiredEndDate, office.Id);
                    if (result.ResultStatus == ResultStatus.Success)
                    {
                        availableRooms.Add(office);
                    }
                }
                if (availableRooms.Count > 0)
                {
                    return new DataResult<IList<Room>>(ResultStatus.Success, availableRooms);
                }
            }
            // not any office available
            return new DataResult<IList<Room>>(ResultStatus.Error, "Not any office available at desired time for your location", null);

        }
        public async Task<IResult> Add(RoomReservationAddDto roomReservationAddDto, int locationId)
        {
            var newReservation = _mapper.Map<RoomReservation>(roomReservationAddDto);

            #region Check Time Interval is During Office Hours
            var result = await CheckHoursForLocation(newReservation, locationId);
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
                    if(inventoryReservations.Count <= 0)
                    { // if room's inventory is not fixed, but have no other reservations - directly add it to current reservation
                        await _unitOfWork.InventoryReservations.AddAsync(new InventoryReservation { RoomReservationGuid = newReservation.RoomReservationGuid, InventoryId = inventory.Id });
                    }
                }

            }
            #endregion

            await _unitOfWork.SaveAsync(); // sava all both reservations and related inventories
            return new Result(ResultStatus.Success, "Reservation added successfully");
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

                return new Result(ResultStatus.Error, $"{location.Name} Office is closed at desired time");
            }
            return new Result(ResultStatus.Success);
        }
        private async Task<IResult> CheckHoursForOffice(DateTime newStartTime, DateTime newEndTime, int roomId)
        {
            var exists = await _unitOfWork.RoomReservations.AnyAsync(x => x.RoomId == roomId &&
                                                                      ((newStartTime >= x.MeetingStartTime && newStartTime <= x.MeetingEndTime) ||
                                                                       (newEndTime >= x.MeetingStartTime && newEndTime <= x.MeetingEndTime) ||
                                                                       (newStartTime.Date == x.MeetingStartTime.Date &&
                                                                       newStartTime <= x.MeetingStartTime && newEndTime >= x.MeetingEndTime)));

            if (exists)
            {
                // Another meeting exists at the desired time interval
                return new Result(ResultStatus.Error, "Another meeting exists at the desired time interval");
            }
            return new Result(ResultStatus.Success);
        }
        private async Task<IResult> CheckAttendantCapacity(int roomId, int attendantCount)
        {
            var office = await _unitOfWork.Rooms.GetAsync(x => x.Id == roomId);
            
            if(office.AttendanceCapacity < attendantCount)
            {
                return new Result(ResultStatus.Error, "Requested attendant count is greater than the office's capacity");
            }
            return new Result(ResultStatus.Success);
        }
        #endregion
    }
}
