using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class AvailabilitySearchDto
    {
        [Required]
        [Display(Name = "Desired Date")]
        public DateTime DesiredDate { get; set; }
        [Required]
        [Display(Name = "Start Hours")]
        public int StartHours { get; set; }
        [Required]
        [Display(Name = "Start Minutes")]
        public int StartMinutes { get; set; }
        [Required]
        [Display(Name = "End Hours")]
        public int EndHours { get; set; }
        [Required]
        [Display(Name = "End Minutes")]
        public int EndMinutes { get; set; }
    }
}
