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
    public class RoomReservationMap : IEntityTypeConfiguration<RoomReservation>
    {
        public void Configure(EntityTypeBuilder<RoomReservation> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.RoomId).IsRequired();
            builder.Property(a => a.MeetingStartTime).IsRequired();
            builder.Property(a => a.MeetingEndTime).IsRequired();
            builder.Property(a => a.AttendantCount).IsRequired();

            builder.ToTable("RoomReservations");

            builder.HasData(
             new RoomReservation
             {
                 Id = 1,
                 RoomId = 2,
                 Description = "Marketing Unit Meeting",
                 MeetingStartTime = new DateTime(2021, 10, 2, 11, 0, 0),
                 MeetingEndTime = new DateTime(2021, 10, 2, 12, 0, 0),
                 AttendantCount = 7,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             });
        }
    }
}
