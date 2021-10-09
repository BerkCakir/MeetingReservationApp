using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Dtos
{
    public class AvailabilitySearchDto
    {
        public DateTime DesiredDate { get; set; }
        public int StartHours { get; set; }
        public int StartMinutes { get; set; }
        public int EndHours { get; set; }
        public int EndMinutes { get; set; }
    }
}
