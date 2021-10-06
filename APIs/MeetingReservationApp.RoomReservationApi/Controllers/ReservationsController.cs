using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.RoomReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IRoomReservationService _roomReservationService;

        public ReservationsController(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRooms(RoomAvailabilitySearchDto roomAvailabilitySearchDto)
        {   
            // returns all available offices for selected location and time interval
            var response = await _roomReservationService.GetAvailableRooms(roomAvailabilitySearchDto, 1);

            return new ObjectResult(response)
            {
                StatusCode = 200
            };
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var response = await _roomReservationService.Add(roomReservationAddDto, 1);

            return new ObjectResult(response)
            {
                StatusCode = 200
            };
        }

    }
}
