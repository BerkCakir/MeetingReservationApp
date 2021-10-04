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
    public class InventoryReservationMap : IEntityTypeConfiguration<InventoryReservation>
    {
        public void Configure(EntityTypeBuilder<InventoryReservation> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.RoomReservationId).IsRequired();
            builder.Property(a => a.InventoryId).IsRequired();
            builder.ToTable("InventoryReservations");

            builder.HasData(
            new InventoryReservation
            {
                Id = 1,
                RoomReservationId = 1,
                InventoryId = 3,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });
        }

    }
}
