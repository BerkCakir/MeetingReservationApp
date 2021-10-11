using MeetingReservationApp.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Controllers
{
    public class RoomReservationController : Controller
    {
        private readonly IRoomReservationService _roomReservationService;

        public RoomReservationController(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _roomReservationService.Get());
        }
    }
}
