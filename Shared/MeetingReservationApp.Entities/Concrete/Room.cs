﻿using MeetingReservationApp.Shared.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Entities.Concrete
{
    public class Room : EntityBase, IEntity
    {
        public string Name { get; set; }
        public int ChairCount { get; set; } // can be zero, representing a room without chairs
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<Inventory> Inventories { get; set; } // inventories fixed in the room
    }
}