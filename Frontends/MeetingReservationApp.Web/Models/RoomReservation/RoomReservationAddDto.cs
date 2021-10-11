using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
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
    }
}
