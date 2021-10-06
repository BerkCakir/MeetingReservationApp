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
            var offices = await _unitOfWork.Rooms.GetAllAsync(c => c.LocationId == locationId);
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

            #region Add Reservation For Room
            newReservation.RoomReservationGuid = Guid.NewGuid();
            await _unitOfWork.RoomReservations.AddAsync(newReservation);
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
            bool exists = await _unitOfWork.RoomReservations.AnyAsync(c => c.RoomId == roomId &&
                                                                      ((newStartTime >= c.MeetingStartTime && newStartTime <= c.MeetingEndTime) ||
                                                                       (newEndTime >= c.MeetingStartTime && newEndTime <= c.MeetingEndTime) ||
                                                                       (newStartTime.Date == c.MeetingStartTime.Date &&
                                                                       newStartTime <= c.MeetingStartTime && newEndTime >= c.MeetingEndTime)));

            if (exists)
            {
                // Another meeting exists at the desired time interval
                return new Result(ResultStatus.Error, "Another meeting exists at the desired time interval");
            }
            return new Result(ResultStatus.Success);
        }
        #endregion
    }
}
