using MeetingReservationApp.Shared.Entites.Abstract;
using System;

namespace MeetingReservationApp.Entities.Concrete
{
    public class InventoryReservation : EntityBase, IEntity
    {
        public Guid RoomReservationGuid { get; set; }
        public RoomReservation RoomReservation { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
