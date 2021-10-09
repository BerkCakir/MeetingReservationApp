using MeetingReservationApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MeetingReservationApp.Data.Concrete.EntityFramework.Mappings
{
    public class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.IsFixed).IsRequired();
            builder.Property(a => a.RoomId).IsRequired();
            builder.Property(a => a.InventoryPurpose).IsRequired();

            builder.HasOne<Room>(a => a.Room).WithMany(c => c.Inventories).HasForeignKey(a => a.RoomId);

            builder.ToTable("Inventories");

            builder.HasData(
             new Inventory
             {
                 Id = 1,
                 RoomId = 1,
                 Name = "Beamer",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Watching,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 2,
                 RoomId = 2,
                 Name = "WhiteBoard",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Drawing,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 3,
                 RoomId = 3,
                 Name = "Television",
                 IsFixed = false,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Watching,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 4,
                 RoomId = 3,
                 Name = "WhiteBoard",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Drawing,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 5,
                 RoomId = 4,
                 Name = "Video Conference Equipment",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.VideoConference,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 6,
                 RoomId = 5,
                 Name = "Television",
                 IsFixed = false,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Watching,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 7,
                 RoomId = 5,
                 Name = "Television",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.VideoConference,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Inventory
             {
                 Id = 8,
                 RoomId = 6,
                 Name = "Beamer",
                 IsFixed = true,
                 InventoryPurpose = Shared.Utilities.Enums.InventoryPurposeType.Watching,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             });
        }
    }
}
