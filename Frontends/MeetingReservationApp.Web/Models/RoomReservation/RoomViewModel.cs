using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class RoomViewModel
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public int AttendanceCapacity { get; set; }
        public bool HasChairs { get; set; }
        public int LocationId { get; set; }
        public LocationViewModel Location { get; set; }
        public ICollection<InventoryViewModel> Inventories { get; set; } // inventories fixed in the room
    }
}
