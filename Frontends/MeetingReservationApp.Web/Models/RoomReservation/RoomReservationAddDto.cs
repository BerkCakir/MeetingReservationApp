using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class RoomReservationAddDto
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DesiredDate { get; set; }
        [Required]
        public int StartHours { get; set; }
        [Required]
        public int StartMinutes { get; set; }
        [Required]
        public int EndHours { get; set; }
        [Required]
        public int EndMinutes { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public int AttendantCount { get; set; }
        [Required]
        public int LocationId { get; set; }

    }
}
