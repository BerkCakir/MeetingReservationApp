using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.InventoryReservation
{
    public class InventoryReservationDto
    {
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public Guid RoomReservationGuid { get; set; }
    }
}
