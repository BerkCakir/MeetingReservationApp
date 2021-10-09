using MeetingReservationApp.Shared.Entites.Abstract;
using MeetingReservationApp.Shared.Utilities.Enums;

namespace MeetingReservationApp.Entities.Concrete
{
    public class Inventory : EntityBase, IEntity
    {
        public string Name { get; set; }
        public bool IsFixed { get; set; } // if resource cannot be moved, set this as true
        public int RoomId { get; set; } // if resource cannot be moved, it should assigned to a room id
        public Room Room { get; set; }
        public InventoryPurposeType InventoryPurpose { get; set; }  // ie. television or beamer = watching, whiteboard = drawing 

    }
}
