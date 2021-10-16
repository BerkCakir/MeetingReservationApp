using MeetingReservationApp.Web.Models.RoomReservation;
using MeetingReservationApp.Web.Results.ComplexTypes;
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
        private readonly IUserService _userService;
        public RoomReservationController(IRoomReservationService roomReservationService, IUserService userService)
        {
            _roomReservationService = roomReservationService;
            _userService = userService;
        }

        public IActionResult Index()
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
            var availabilitySearchDto = new AvailabilitySearchDto { DesiredDate = DateTime.Now.Date, StartHours = 10, StartMinutes = 0, EndHours = 11, EndMinutes = 0 };
            return View(availabilitySearchDto);
        }

        [HttpPost]
        public async Task<IActionResult> Search(AvailabilitySearchDto availabilitySearchDto)
        {
            TempData["DesiredDate"] = availabilitySearchDto.DesiredDate;
            TempData["StartHours"] = availabilitySearchDto.StartHours;
            TempData["StartMinutes"] = availabilitySearchDto.StartMinutes;
            TempData["EndHours"] = availabilitySearchDto.EndHours;
            TempData["EndMinutes"] = availabilitySearchDto.EndMinutes;

            var result = await _roomReservationService.GetAvailability(availabilitySearchDto, _userService.GetUser().Result.Location);
            if (result.ResultStatus != ResultStatus.Success)
            {
                TempData["errorMessage"] = result.Message;
            }
            return View(result.Data);
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

            if (!ModelState.IsValid)
            {
                return View(roomReservationAddDto);
            }
            roomReservationAddDto.LocationId = _userService.GetUser().Result.Location;
            var result = await _roomReservationService.Add(roomReservationAddDto);
            if (result.ResultStatus != ResultStatus.Success)
            {
                TempData["errorMessage"] = result.Message;
                return View(roomReservationAddDto);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
