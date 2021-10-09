using MeetingReservationApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasOne<Room>(a => a.Room).WithMany(c => c.RoomReservations).HasForeignKey(a => a.RoomId);

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
                 ModifiedDate = DateTime.Now,
                 RoomReservationGuid = Guid.Parse("019ee26d-5111-4d96-abfa-5bd6c966d529")
             });
        }
    }
}
