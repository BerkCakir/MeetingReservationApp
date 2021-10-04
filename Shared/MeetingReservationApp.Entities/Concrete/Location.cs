using MeetingReservationApp.Shared.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Concrete
{
    public class Location : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int OfficeStartHours { get; set; }
        public int OfficeStartMinutes { get; set; }
        public int OfficeEndHours { get; set; }
        public int OfficeEndMinutes { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
