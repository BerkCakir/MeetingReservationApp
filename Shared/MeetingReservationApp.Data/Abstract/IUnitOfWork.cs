﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ILocationRepository Locations { get; }
        IRoomRepository Rooms { get; }
        IInventoryRepository Inventories { get; }
        IRoomReservationRepository RoomReservations { get; }
        IInventoryReservationRepository InventoryReservations { get; }
        Task<int> SaveAsync();
    }
}
