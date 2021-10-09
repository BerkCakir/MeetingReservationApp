using MeetingReservationApp.Shared.Entites.Abstract;
using System.Collections.Generic;

namespace MeetingReservationApp.Entities.Concrete
{
    public class Room : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int AttendanceCapacity { get; set; }
        public bool HasChairs { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<Inventory> Inventories { get; set; } // inventories fixed in the room
    }
}
