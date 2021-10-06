using MeetingReservationApp.Shared.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Concrete
{
    public class InventoryReservation : EntityBase, IEntity
    {
        public Guid RoomReservationGuid { get; set; }
        public RoomReservation RoomReservation { get; set; }
        public int InventoryId { get; set; }
    }
}
