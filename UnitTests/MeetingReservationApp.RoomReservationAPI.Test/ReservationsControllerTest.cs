using MeetingReservationApp.Entities.Concrete;
using MeetingReservationApp.Entities.Dtos;
using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.RoomReservationApi.Controllers;
using MeetingReservationApp.Shared.Utilities.Messages;
using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using MeetingReservationApp.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeetingReservationApp.RoomReservationAPI.Test
{
    public class ReservationsControllerTest
    {
        private readonly Mock<IRoomReservationService> _mockRepo;

        private readonly ReservationsController _controller;

        private List<Location> locations;

        private List<Room> rooms;
        public ReservationsControllerTest()
        {
            _mockRepo = new Mock<IRoomReservationService>();
            _controller = new ReservationsController(_mockRepo.Object);

            locations = new List<Location>() { new Location { Id = 1, Name = "Amsterdam", OfficeStartHours=8, OfficeStartMinutes=30,
                OfficeEndHours=17,OfficeEndMinutes = 0}};

            rooms = new List<Room>() { new Room { Id = 1, Name = "Room 1", AttendanceCapacity = 5 ,HasChairs = false, LocationId = 1},
            new Room { Id = 2, Name = "Room 2", AttendanceCapacity = 10 ,HasChairs = true, LocationId = 1}};
        }

        [Theory]
        [InlineData("abcabc", 10, 0, 11, 30, 1)]
        public async void GetAvailableRooms_ActionExecutes_ReturnBadRequest(string desiredDate, int startHours, int startMinutes, int endHours, int endMinutes, int locationId)
        {
            var result = await _controller.GetAvailableRooms(desiredDate, startHours, startMinutes, endHours, endMinutes, locationId);
            var response = Assert.IsType<BadRequestResult>(result);
            Assert.Equal<int>(400, response.StatusCode);
        }
        [Theory]
        [InlineData(12, 0, 13, 0, 1)]
        public async void GetAvailableRooms_ActionExecutes_ReturnOfficeList(int startHours, int startMinutes, int endHours, int endMinutes,
                                                                            int locationId)
        {
            var availabilitySearchDto =
            new AvailabilitySearchDto
            {
                DesiredDate = DateTime.Now.Date,
                StartHours = startHours,
                StartMinutes = startMinutes,
                EndHours = endHours,
                EndMinutes = endMinutes
            };

            _mockRepo.Setup(x => x.GetAvailableRooms(DateTime.Now.Date.ToString("dd-MM-yyyy"), startHours, startMinutes, endHours, endMinutes, locationId)).ReturnsAsync(new DataResult<IList<Room>>(ResultStatus.Success, rooms));

            var response = await _controller.GetAvailableRooms(DateTime.Now.Date.ToString("dd-MM-yyyy"), startHours, startMinutes, endHours, endMinutes, locationId);

            var okResult = Assert.IsType<ObjectResult>(response);

            Assert.Equal<int>(2, ((DataResult<IList<Room>>)okResult.Value).Data.Count);

        }
    }
}
