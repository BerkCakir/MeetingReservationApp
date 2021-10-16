using MeetingReservationApp.Web.Models.InventoryReservation;
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
    public class InventoryReservationController : Controller
    {
        private readonly IInventoryReservationService _inventoryReservationService;
        private readonly IRoomReservationService _roomReservationService;
        private readonly IUserService _userService;

        public InventoryReservationController(IInventoryReservationService inventoryReservationService, IRoomReservationService roomReservationService, IUserService userService)
        {
            _inventoryReservationService = inventoryReservationService;
            _roomReservationService = roomReservationService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _roomReservationService.GetAll(_userService.GetUser().Result.Location);

            return View(result);
        }
        public async Task<IActionResult> Add(string Id)
        {
            var inv = await _inventoryReservationService.GetAll(_userService.GetUser().Result.Location);
            ViewBag.inventoryList = new SelectList(inv, "Id", "Name");
            InventoryReservationDto inventoryReservationDto = new InventoryReservationDto { RoomReservationGuid = Guid.Parse(Id)};
            return View(inventoryReservationDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add(InventoryReservationDto inventoryReservationDto)
        {
            var inv = await _inventoryReservationService.GetAll(_userService.GetUser().Result.Location);
            ViewBag.inventoryList = new SelectList(inv, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View(inventoryReservationDto);
            }
            var result= await _inventoryReservationService.Add(inventoryReservationDto);
            if (result.ResultStatus != ResultStatus.Success)
            {
                TempData["errorMessage"] = result.Message;
                return View(inventoryReservationDto);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
