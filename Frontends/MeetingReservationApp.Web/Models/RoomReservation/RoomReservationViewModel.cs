using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web.Models.RoomReservation
{
    public class RoomReservationViewModel
    {
        public Guid RoomReservationGuid { get; set; }
        public string Description { get; set; }
        public DateTime MeetingStartTime { get; set; }
        public DateTime MeetingEndTime { get; set; }
        public int AttendantCount { get; set; }
        public RoomViewModel Room { get; set; }
        public ICollection<InventoryReservationViewModel> InventoryReservations { get; set; }
    }
}
