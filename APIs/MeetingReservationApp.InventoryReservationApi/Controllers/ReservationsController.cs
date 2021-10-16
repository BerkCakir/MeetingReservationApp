using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeetingReservationApp.InventoryReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : CustomControllerBase
    {
        private readonly IInventoryReservationService _inventoryReservationService;

        public ReservationsController(IInventoryReservationService inventoryReservationService)
        {
            _inventoryReservationService = inventoryReservationService;
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetAll(int locationId)
        {
            // returns all available offices for selected location and time interval
            var response = await _inventoryReservationService.GetAll(locationId);
            return CreateResultWithData(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryReservationAddDto inventoryReservationAddDto)
        {
            var response = await _inventoryReservationService.Add(inventoryReservationAddDto);
            return CreateResult(response);
        }
    }
}

