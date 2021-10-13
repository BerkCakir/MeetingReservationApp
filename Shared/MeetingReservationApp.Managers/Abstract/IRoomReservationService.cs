using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Abstract
{
    public interface IRoomReservationService
    {
        Task<IDataResult<IList<Room>>> GetAvailableRooms(AvailabilitySearchDto roomAvailabilitySearchDto, int locationId);
        Task<IResult> Add(RoomReservationAddDto roomReservationAddDto, int locationId);
        Task<IDataResult<IList<RoomReservation>>> GetAllAsync(int locationId);
    }
}
