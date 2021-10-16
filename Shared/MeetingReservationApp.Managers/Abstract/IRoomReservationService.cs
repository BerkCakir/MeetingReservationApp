using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingReservationApp.Managers.Abstract
{
    public interface IRoomReservationService
    {
        Task<IDataResult<IList<Room>>> GetAvailableRooms(string desiredDate, int startHours, int startMinutes, int endHours, int endMinutes, int locationId);
        Task<IResult> Add(RoomReservationAddDto roomReservationAddDto);
        Task<IDataResult<IList<RoomReservation>>> GetAllAsync(int locationId);
    }
}
