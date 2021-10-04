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
            builder.Property(a => a.ChairCount).IsRequired();

            builder.HasOne<Location>(a => a.Location).WithMany(c => c.Rooms).HasForeignKey(a => a.LocationId);

            builder.ToTable("Rooms");

            builder.HasData(
             new Room
             {
                 Id = 1,
                 LocationId = 1,
                 Name = "Meeting Room 1",
                 ChairCount = 10,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 2,
                 LocationId = 1,
                 Name = "Meeting Room 2",
                 ChairCount = 0,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 3,
                 LocationId = 1,
                 Name = "Meeting Room 3",
                 ChairCount = 5,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 4,
                 LocationId = 2,
                 Name = "Meeting Room A",
                 ChairCount = 20,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Room
             {
                 Id = 5,
                 LocationId = 2,
                 Name = "Meeting Room B",
                 ChairCount = 5,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
           new Room
           {
               Id = 6,
               LocationId = 2,
               Name = "Meeting Room C",
               ChairCount = 0,
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           });
        }
    }
}
