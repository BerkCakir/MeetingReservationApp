using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFixed { get; set; } // if resource cannot be moved, set this as true
        public int RoomId { get; set; }
    }
}
