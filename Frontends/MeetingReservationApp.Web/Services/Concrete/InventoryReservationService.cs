using MeetingReservationApp.Web.Models.InventoryReservation;
using MeetingReservationApp.Web.Models.RoomReservation;
using MeetingReservationApp.Web.Results.Concrete;
using MeetingReservationApp.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Services.Concrete
{
    public class InventoryReservationService : IInventoryReservationService
    {
        private readonly HttpClient _httpClient;

        public InventoryReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result> Add(InventoryReservationDto inventoryReservationDto)
        {
            var response = await _httpClient.PostAsJsonAsync<InventoryReservationDto>("reservations", inventoryReservationDto);

            var result = await response.Content.ReadFromJsonAsync<Result>();

            return result;
        }

        public async Task<IList<InventoryViewModel>> GetAll(int locationId)
        {
            var response = await _httpClient.GetAsync($"reservations/{locationId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<DataResult<List<InventoryViewModel>>>();

            return responseSuccess.Data;
        }
    }
}
