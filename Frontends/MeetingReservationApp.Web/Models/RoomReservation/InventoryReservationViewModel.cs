using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class InventoryReservationViewModel
    {
        public int InventoryId { get; set; }
        public InventoryViewModel Inventory { get; set; }
    }
}
