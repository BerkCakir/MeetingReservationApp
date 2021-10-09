using System;

namespace MeetingReservationApp.Entities.Dtos
{
    public class InventoryReservationAddDto
    {
        public int InventoryId { get; set; }
        public Guid RoomReservationGuid { get; set; }
    }
}
