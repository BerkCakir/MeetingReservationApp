using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAvailableRooms(string desiredDate, int startHours, int startMinutes, int endHours, int endMinutes)
        {
            AvailabilitySearchDto availabilitySearchDto = new AvailabilitySearchDto
            {
                DesiredDate = Convert.ToDateTime(desiredDate),
                StartHours = startHours,
                StartMinutes = startMinutes,
                EndHours = endHours,
                EndMinutes = endMinutes
            };
            // returns all available offices for selected location and time interval
            var response = await _roomReservationService.GetAvailableRooms(availabilitySearchDto, 1);
            return CreateResultWithData(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {  
            // returns all reservations related to location
            var response = await _roomReservationService.GetAllAsync(1);
            return CreateResultWithData(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var response = await _roomReservationService.Add(roomReservationAddDto, 1);
            return CreateResult(response);
        }
    }
}
