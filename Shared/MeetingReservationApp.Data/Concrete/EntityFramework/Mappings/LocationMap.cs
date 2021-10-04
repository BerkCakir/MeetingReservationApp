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
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.OfficeStartHours).IsRequired();
            builder.Property(a => a.OfficeStartMinutes).IsRequired();
            builder.Property(a => a.OfficeEndHours).IsRequired();
            builder.Property(a => a.OfficeEndMinutes).IsRequired();

            builder.ToTable("Locations");

            builder.HasData(
             new Location
             {
                 Id = 1,
                 Name = "Amsterdam",
                 OfficeStartHours = 8,
                 OfficeStartMinutes = 30,
                 OfficeEndHours = 17,
                 OfficeEndMinutes = 0,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             },
             new Location
             {
                 Id = 2,
                 Name = "Berlin",
                 OfficeStartHours = 8,
                 OfficeStartMinutes = 30,
                 OfficeEndHours = 20,
                 OfficeEndMinutes = 0,
                 CreatedDate = DateTime.Now,
                 ModifiedDate = DateTime.Now
             }
             );
        }
    }
}
