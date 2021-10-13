using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.InventoryReservation
{
    public class InventoryReservationDto
    {
        public int InventoryId { get; set; }
        public Guid RoomReservationGuid { get; set; }
    }
}
