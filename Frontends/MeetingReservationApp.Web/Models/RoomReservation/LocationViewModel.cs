using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class LocationViewModel
    {
        public string Name { get; set; }
        public int OfficeStartHours { get; set; }
        public int OfficeStartMinutes { get; set; }
        public int OfficeEndHours { get; set; }
    }
}
