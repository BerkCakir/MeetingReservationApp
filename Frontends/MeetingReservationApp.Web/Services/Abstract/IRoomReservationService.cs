using MeetingReservationApp.Web.Models.RoomReservation;
using MeetingReservationApp.Web.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Services.Abstract
{
    public interface IRoomReservationService
    {
        Task<DataResult<IList<RoomViewModel>>> GetAvailability(AvailabilitySearchDto availabilitySearchDto, int locationId);

        Task<Result> Add(RoomReservationAddDto roomReservationAddDto);
        Task<IList<RoomReservationViewModel>> GetAll(int locationId);
    }
}
