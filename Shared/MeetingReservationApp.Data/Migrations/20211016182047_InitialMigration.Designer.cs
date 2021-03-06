// <auto-generated />
using System;
using MeetingReservationApp.Data.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeetingReservationApp.Data.Migrations
{
    [DbContext(typeof(MeetingReservationAppContext))]
    [Migration("20211016182047_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InventoryPurpose")
                        .HasColumnType("int");

                    b.Property<bool>("IsFixed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Inventories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3881),
                            InventoryPurpose = 2,
                            IsFixed = true,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3894),
                            Name = "Beamer",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 9,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3904),
                            InventoryPurpose = 4,
                            IsFixed = false,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3905),
                            Name = "Marker Pen Set",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3909),
                            InventoryPurpose = 1,
                            IsFixed = true,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3910),
                            Name = "WhiteBoard",
                            RoomId = 2
                        },
                        new
                        {
                            Id = 10,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3913),
                            InventoryPurpose = 3,
                            IsFixed = false,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3914),
                            Name = "Video Conference Equipment",
                            RoomId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3917),
                            InventoryPurpose = 2,
                            IsFixed = false,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3918),
                            Name = "Television",
                            RoomId = 3
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3921),
                            InventoryPurpose = 1,
                            IsFixed = true,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3922),
                            Name = "WhiteBoard",
                            RoomId = 3
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3926),
                            InventoryPurpose = 3,
                            IsFixed = false,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3927),
                            Name = "Video Conference Equipment",
                            RoomId = 4
                        },
                        new
                        {
                            Id = 6,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3930),
                            InventoryPurpose = 2,
                            IsFixed = false,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3931),
                            Name = "Television",
                            RoomId = 5
                        },
                        new
                        {
                            Id = 7,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3934),
                            InventoryPurpose = 1,
                            IsFixed = true,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3935),
                            Name = "WhiteBoard",
                            RoomId = 5
                        },
                        new
                        {
                            Id = 8,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3938),
                            InventoryPurpose = 2,
                            IsFixed = true,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 191, DateTimeKind.Local).AddTicks(3939),
                            Name = "Beamer",
                            RoomId = 6
                        });
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.InventoryReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoomReservationGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("RoomReservationGuid");

                    b.ToTable("InventoryReservations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 198, DateTimeKind.Local).AddTicks(5749),
                            InventoryId = 2,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 198, DateTimeKind.Local).AddTicks(5762),
                            RoomReservationGuid = new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529")
                        });
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OfficeEndHours")
                        .HasColumnType("int");

                    b.Property<int>("OfficeEndMinutes")
                        .HasColumnType("int");

                    b.Property<int>("OfficeStartHours")
                        .HasColumnType("int");

                    b.Property<int>("OfficeStartMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 185, DateTimeKind.Local).AddTicks(766),
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 185, DateTimeKind.Local).AddTicks(1025),
                            Name = "Amsterdam",
                            OfficeEndHours = 17,
                            OfficeEndMinutes = 0,
                            OfficeStartHours = 8,
                            OfficeStartMinutes = 30
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 185, DateTimeKind.Local).AddTicks(1276),
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 185, DateTimeKind.Local).AddTicks(1277),
                            Name = "Berlin",
                            OfficeEndHours = 20,
                            OfficeEndMinutes = 0,
                            OfficeStartHours = 8,
                            OfficeStartMinutes = 30
                        });
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttendanceCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasChairs")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AttendanceCapacity = 10,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3351),
                            HasChairs = true,
                            LocationId = 1,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3364),
                            Name = "Meeting Room 1"
                        },
                        new
                        {
                            Id = 2,
                            AttendanceCapacity = 12,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3373),
                            HasChairs = true,
                            LocationId = 1,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3374),
                            Name = "Meeting Room 2"
                        },
                        new
                        {
                            Id = 3,
                            AttendanceCapacity = 5,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3377),
                            HasChairs = false,
                            LocationId = 1,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3378),
                            Name = "Meeting Room 3"
                        },
                        new
                        {
                            Id = 4,
                            AttendanceCapacity = 20,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3382),
                            HasChairs = true,
                            LocationId = 2,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3383),
                            Name = "Meeting Room A"
                        },
                        new
                        {
                            Id = 5,
                            AttendanceCapacity = 5,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3385),
                            HasChairs = false,
                            LocationId = 2,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3386),
                            Name = "Meeting Room B"
                        },
                        new
                        {
                            Id = 6,
                            AttendanceCapacity = 25,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3389),
                            HasChairs = true,
                            LocationId = 2,
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 189, DateTimeKind.Local).AddTicks(3390),
                            Name = "Meeting Room C"
                        });
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.RoomReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttendantCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("MeetingEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MeetingStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomReservationGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomReservations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AttendantCount = 7,
                            CreatedDate = new DateTime(2021, 10, 16, 21, 20, 47, 193, DateTimeKind.Local).AddTicks(3814),
                            Description = "Marketing Unit Meeting",
                            MeetingEndTime = new DateTime(2021, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            MeetingStartTime = new DateTime(2021, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(2021, 10, 16, 21, 20, 47, 193, DateTimeKind.Local).AddTicks(3828),
                            RoomId = 2,
                            RoomReservationGuid = new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529")
                        });
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Inventory", b =>
                {
                    b.HasOne("MeetingReservationApp.Entities.Concrete.Room", "Room")
                        .WithMany("Inventories")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.InventoryReservation", b =>
                {
                    b.HasOne("MeetingReservationApp.Entities.Concrete.Inventory", "Inventory")
                        .WithMany("InventoryReservations")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MeetingReservationApp.Entities.Concrete.RoomReservation", "RoomReservation")
                        .WithMany("InventoryReservations")
                        .HasForeignKey("RoomReservationGuid")
                        .HasPrincipalKey("RoomReservationGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("RoomReservation");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Room", b =>
                {
                    b.HasOne("MeetingReservationApp.Entities.Concrete.Location", "Location")
                        .WithMany("Rooms")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.RoomReservation", b =>
                {
                    b.HasOne("MeetingReservationApp.Entities.Concrete.Room", "Room")
                        .WithMany("RoomReservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Inventory", b =>
                {
                    b.Navigation("InventoryReservations");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Location", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.Room", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("RoomReservations");
                });

            modelBuilder.Entity("MeetingReservationApp.Entities.Concrete.RoomReservation", b =>
                {
                    b.Navigation("InventoryReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
