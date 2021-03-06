using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
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

        [HttpGet("availablerooms")]
        public async Task<IActionResult> GetAvailableRooms(string desiredDate, int startHours, int startMinutes, int endHours, int endMinutes, int locationId)
        {
            DateTime foundDate;
            if (!DateTime.TryParseExact(desiredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out foundDate) ||
                (startHours < 0 || endHours < 0 || startMinutes < 0 || endMinutes < 0))
            {
                return BadRequest();
            }
            // returns all available offices for selected location and time interval
            var response = await _roomReservationService.GetAvailableRooms(desiredDate,  startHours,  startMinutes,  endHours,  endMinutes, locationId);
            return CreateResultWithData(response);
        }
        [HttpGet("{locationId}")]
        public async Task<IActionResult> Get(int locationId)
        {  
            // returns all reservations related to location
            var response = await _roomReservationService.GetAllAsync(locationId);
            return CreateResultWithData(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var response = await _roomReservationService.Add(roomReservationAddDto);
            return CreateResult(response);
        }
    }
}
