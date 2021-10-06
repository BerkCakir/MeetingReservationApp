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
        #region Private Bussiness Methods
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
