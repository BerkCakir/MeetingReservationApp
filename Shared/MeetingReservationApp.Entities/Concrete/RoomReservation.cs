using MeetingReservationApp.Shared.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Concrete
{
    public class RoomReservation : EntityBase, IEntity
    {
        public Guid RoomReservationGuid { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public DateTime MeetingStartTime { get; set; }
        public DateTime MeetingEndTime { get; set; }
        public int AttendantCount { get; set; }
        public ICollection<InventoryReservation> InventoryReservation { get; set; }
    }
}
