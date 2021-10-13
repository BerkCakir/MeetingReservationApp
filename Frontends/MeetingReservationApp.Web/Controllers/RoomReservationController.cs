using MeetingReservationApp.Web.Models.RoomReservation;
using MeetingReservationApp.Web.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Controllers
{
    [Authorize]
    public class RoomReservationController : Controller
    {
        private readonly IRoomReservationService _roomReservationService;

        public RoomReservationController(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }
        public IActionResult Index()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            for(int i=0; i<25;i++)
            {
                listItems.Add(new SelectListItem { Text = i.ToString().PadLeft(2,'0'), Value = i.ToString() });
            }
            ViewBag.HoursList = listItems; 
            listItems = new List<SelectListItem>();
            for (int i = 0; i < 60; i++)
            {
                listItems.Add(new SelectListItem { Text = i.ToString().PadLeft(2, '0'), Value = i.ToString() });
            }
            ViewBag.MinutesList = listItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(AvailabilitySearchDto availabilitySearchDto)
        {
            TempData["DesiredDate"] = availabilitySearchDto.DesiredDate;
            TempData["StartHours"] = availabilitySearchDto.StartHours;
            TempData["StartMinutes"] = availabilitySearchDto.StartMinutes;
            TempData["EndHours"] = availabilitySearchDto.EndHours;
            TempData["EndMinutes"] = availabilitySearchDto.EndMinutes;

            return View(await _roomReservationService.GetAvailability(availabilitySearchDto));
        }
        public IActionResult Add(string Id)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            for (int i = 0; i < 25; i++)
            {
                listItems.Add(new SelectListItem { Text = i.ToString().PadLeft(2, '0'), Value = i.ToString() });
            }
            ViewBag.HoursList = listItems;
            listItems = new List<SelectListItem>();
            for (int i = 0; i < 60; i++)
            {
                listItems.Add(new SelectListItem { Text = i.ToString().PadLeft(2, '0'), Value = i.ToString() });
            }
            ViewBag.MinutesList = listItems;

            RoomReservationAddDto roomReservationAddDto = new RoomReservationAddDto();
            roomReservationAddDto.RoomId = Convert.ToInt32(Id);
            roomReservationAddDto.DesiredDate = (DateTime)TempData["DesiredDate"];
            roomReservationAddDto.StartHours = (int)TempData["StartHours"];
            roomReservationAddDto.StartMinutes = (int)TempData["StartMinutes"];
            roomReservationAddDto.EndHours = (int)TempData["EndHours"];
            roomReservationAddDto.EndMinutes = (int)TempData["EndMinutes"];
            return View(roomReservationAddDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoomReservationAddDto roomReservationAddDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _roomReservationService.Add(roomReservationAddDto);

            return RedirectToAction(nameof(Index));
        }
    }
}
