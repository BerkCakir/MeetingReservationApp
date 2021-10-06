using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Abstract
{
    public interface IRoomReservationService
    {
        Task<IDataResult<IList<Room>>> GetAvailableRooms(RoomAvailabilitySearchDto roomAvailabilitySearchDto, int locationId);
    }
}
