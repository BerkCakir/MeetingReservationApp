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

        public async Task<IList<RoomViewModel>> GetAvailability(AvailabilitySearchDto availabilitySearchDto)
        {
            string desiredDate = availabilitySearchDto.DesiredDate.ToString("dd-MM-yyyy");
            int startHours = availabilitySearchDto.StartHours;
            int startMinutes = availabilitySearchDto.StartMinutes;
            int endHours = availabilitySearchDto.EndHours;
            int endMinutes = availabilitySearchDto.EndMinutes;
            var response = await _httpClient.GetAsync($"reservations/availablerooms?desiredDate={desiredDate}&startHours={startHours}&startMinutes={startMinutes}&endHours={endHours}&endMinutes={endMinutes}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<DataResult<List<RoomViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<Result> Add(RoomReservationAddDto roomReservationAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync<RoomReservationAddDto>("reservations", roomReservationAddDto);

            var result = await response.Content.ReadFromJsonAsync<Result>();

            return result;
        }

        public async Task<IList<RoomReservationViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("reservations");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<DataResult<List<RoomReservationViewModel>>>();

            return responseSuccess.Data;
        }
    }
}
