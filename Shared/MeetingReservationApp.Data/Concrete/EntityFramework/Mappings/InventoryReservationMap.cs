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
            builder.Property(a => a.RoomReservationGuid).IsRequired();
            builder.Property(a => a.InventoryId).IsRequired();
            builder.ToTable("InventoryReservations");


            builder.HasOne<RoomReservation>(a => a.RoomReservation).WithMany(c => c.InventoryReservation)
                                            .HasPrincipalKey(a => a.RoomReservationGuid).HasForeignKey(a => a.RoomReservationGuid);

            builder.HasData(
            new InventoryReservation
            {
                Id = 1,
                RoomReservationGuid = Guid.Parse("019ee26d-5111-4d96-abfa-5bd6c966d529"),
                InventoryId = 2,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });
        }

    }
}
