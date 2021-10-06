using MeetingReservationApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Mappings
{
    public class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.LocationId).IsRequired();
            builder.Property(a => a.AttendanceCapacity).IsRequired();

            builder.HasOne<Location>(a => a.Location).WithMany(c => c.Rooms).HasForeignKey(a => a.LocationId);

            builder.ToTable("Rooms");

            builder.HasData(
             new Room
             {
                 Id = 1,
                 LocationId = 1,
                 Name = "Meeting Room 1",
                 AttendanceCapacity = 10,
                 HasChairs = true,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 2,
                 LocationId = 1,
                 Name = "Meeting Room 2",
                 AttendanceCapacity = 12,
                 HasChairs = true,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 3,
                 LocationId = 1,
                 Name = "Meeting Room 3",
                 AttendanceCapacity = 5,
                 HasChairs = false,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 4,
                 LocationId = 2,
                 Name = "Meeting Room A",
                 AttendanceCapacity = 20,
                 HasChairs = true,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 5,
                 LocationId = 2,
                 Name = "Meeting Room B",
                 AttendanceCapacity = 5,
                 HasChairs = false,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
           new Room
           {
               Id = 6,
               LocationId = 2,
               Name = "Meeting Room C",
               AttendanceCapacity = 25,
               HasChairs = true,
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           });;
        }
    }
}
