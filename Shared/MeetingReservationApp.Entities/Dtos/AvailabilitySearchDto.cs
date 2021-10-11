using Microsoft.AspNetCore.Mvc;
using System;

namespace MeetingReservationApp.Entities.Dtos
{
    [BindProperties]
    public class AvailabilitySearchDto
    {
        public DateTime DesiredDate { get; set; }
        public int StartHours { get; set; }
        public int StartMinutes { get; set; }
        public int EndHours { get; set; }
        public int EndMinutes { get; set; }
    }
}
