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
    public class RoomReservationService : IRoomReservationService
    {
        private readonly HttpClient _httpClient;

        public RoomReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<RoomViewModel>> Get()
        {
            var response = await _httpClient.GetAsync("reservations");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<DataResult<List<RoomViewModel>>>();
            return responseSuccess.Data;
        }
    }
}
