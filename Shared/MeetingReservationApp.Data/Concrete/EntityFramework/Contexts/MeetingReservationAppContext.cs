using MeetingReservationApp.Data.Concrete.EntityFramework.Mappings;
using MeetingReservationApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Contexts
{
    public class MeetingReservationAppContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<InventoryReservation> InventoryReservations { get; set; }

        public MeetingReservationAppContext(DbContextOptions<MeetingReservationAppContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LocationMap());
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new InventoryMap());
            modelBuilder.ApplyConfiguration(new RoomReservationMap());
            modelBuilder.ApplyConfiguration(new InventoryReservationMap());
        }
    }
}
