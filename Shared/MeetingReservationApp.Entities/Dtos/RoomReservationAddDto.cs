using Microsoft.AspNetCore.Mvc;
using System;

namespace MeetingReservationApp.Entities.Dtos
{
    [BindProperties]
    public class RoomReservationAddDto
    {
        public int RoomId { get; set; }
        public string Description { get; set; }
        public DateTime DesiredDate { get; set; }
        public int StartHours { get; set; }
        public int StartMinutes { get; set; }
        public int EndHours { get; set; }
        public int EndMinutes { get; set; }
        public int AttendantCount { get; set; }
        public int LocationId { get; set; }
    }
}
