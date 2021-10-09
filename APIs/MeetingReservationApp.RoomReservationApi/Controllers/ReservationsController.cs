using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeetingReservationApp.RoomReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : CustomControllerBase
    {
        private readonly IRoomReservationService _roomReservationService;

        public ReservationsController(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRooms(AvailabilitySearchDto roomAvailabilitySearchDto)
        {
            // returns all available offices for selected location and time interval
            var response = await _roomReservationService.GetAvailableRooms(roomAvailabilitySearchDto, 1);
            return CreateResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var response = await _roomReservationService.Add(roomReservationAddDto, 1);
            return CreateResult(response);
        }
    }
}
