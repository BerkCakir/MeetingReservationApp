using MeetingReservationApp.Data.Abstract;
using MeetingReservationApp.Data.Concrete.EntityFramework.Contexts;
using MeetingReservationApp.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeetingReservationAppContext _context;
        private EfRoomReservationRepository _roomReservationRepository;
        private EfInventoryReservationRepository _inventoryReservationRepository;
        private EfRoomRepository _roomRepository;
        private EfLocationRepository _locationRepository;
        private EfInventoryRepository _inventoryRepository;

        public UnitOfWork(MeetingReservationAppContext context)
        {
            _context = context;
        }

        public IRoomReservationRepository RoomReservations => _roomReservationRepository ?? new EfRoomReservationRepository(_context);

        public IInventoryReservationRepository InventoryReservations => _inventoryReservationRepository ?? new EfInventoryReservationRepository(_context);
        public IRoomRepository Rooms => _roomRepository ?? new EfRoomRepository(_context);

        public ILocationRepository Locations => _locationRepository ?? new EfLocationRepository(_context);
        public IInventoryRepository Inventories => _inventoryRepository ?? new EfInventoryRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
