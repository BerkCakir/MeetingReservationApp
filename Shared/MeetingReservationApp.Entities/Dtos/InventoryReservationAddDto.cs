using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Dtos
{
    public class InventoryReservationAddDto
    {
        public int InventoryId { get; set; }
        public Guid RoomReservationGuid { get; set; }
    }
}
