using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingReservationApp.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OfficeStartHours = table.Column<int>(type: "int", nullable: false),
                    OfficeStartMinutes = table.Column<int>(type: "int", nullable: false),
                    OfficeEndHours = table.Column<int>(type: "int", nullable: false),
                    OfficeEndMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MeetingStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttendantCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservations", x => x.Id);
                    table.UniqueConstraint("AK_RoomReservations_RoomReservationGuid", x => x.RoomReservationGuid);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttendanceCapacity = table.Column<int>(type: "int", nullable: false),
                    HasChairs = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryReservations_RoomReservations_RoomReservationGuid",
                        column: x => x.RoomReservationGuid,
                        principalTable: "RoomReservations",
                        principalColumn: "RoomReservationGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    InventoryPurpose = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "OfficeEndHours", "OfficeEndMinutes", "OfficeStartHours", "OfficeStartMinutes" },
                values: new object[] { 1, new DateTime(2021, 10, 7, 0, 12, 56, 563, DateTimeKind.Local).AddTicks(2926), new DateTime(2021, 10, 7, 0, 12, 56, 563, DateTimeKind.Local).AddTicks(3186), "Amsterdam", 17, 0, 8, 30 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "OfficeEndHours", "OfficeEndMinutes", "OfficeStartHours", "OfficeStartMinutes" },
                values: new object[] { 2, new DateTime(2021, 10, 7, 0, 12, 56, 563, DateTimeKind.Local).AddTicks(3436), new DateTime(2021, 10, 7, 0, 12, 56, 563, DateTimeKind.Local).AddTicks(3438), "Berlin", 20, 0, 8, 30 });

            migrationBuilder.InsertData(
                table: "RoomReservations",
                columns: new[] { "Id", "AttendantCount", "CreatedDate", "Description", "MeetingEndTime", "MeetingStartTime", "ModifiedDate", "RoomId", "RoomReservationGuid" },
                values: new object[] { 1, 7, new DateTime(2021, 10, 7, 0, 12, 56, 571, DateTimeKind.Local).AddTicks(2147), "Marketing Unit Meeting", new DateTime(2021, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 7, 0, 12, 56, 571, DateTimeKind.Local).AddTicks(2160), 2, new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529") });

            migrationBuilder.InsertData(
                table: "InventoryReservations",
                columns: new[] { "Id", "CreatedDate", "InventoryId", "ModifiedDate", "RoomReservationGuid" },
                values: new object[] { 1, new DateTime(2021, 10, 7, 0, 12, 56, 576, DateTimeKind.Local).AddTicks(9454), 2, new DateTime(2021, 10, 7, 0, 12, 56, 576, DateTimeKind.Local).AddTicks(9467), new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529") });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AttendanceCapacity", "CreatedDate", "HasChairs", "LocationId", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(6987), true, 1, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7001), "Meeting Room 1" },
                    { 2, 12, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7009), true, 1, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7011), "Meeting Room 2" },
                    { 3, 5, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7014), false, 1, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7015), "Meeting Room 3" },
                    { 4, 20, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7018), true, 2, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7019), "Meeting Room A" },
                    { 5, 5, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7023), false, 2, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7024), "Meeting Room B" },
                    { 6, 25, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7027), true, 2, new DateTime(2021, 10, 7, 0, 12, 56, 567, DateTimeKind.Local).AddTicks(7028), "Meeting Room C" }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedDate", "InventoryPurpose", "IsFixed", "ModifiedDate", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7831), 2, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7845), "Beamer", 1 },
                    { 2, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7855), 1, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7856), "WhiteBoard", 2 },
                    { 3, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7859), 2, false, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7860), "Television", 3 },
                    { 4, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7863), 1, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7864), "WhiteBoard", 3 },
                    { 5, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7868), 3, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7869), "Video Conference Equipment", 4 },
                    { 6, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7872), 2, false, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7873), "Television", 5 },
                    { 7, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7876), 3, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7877), "Television", 5 },
                    { 8, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7880), 2, true, new DateTime(2021, 10, 7, 0, 12, 56, 569, DateTimeKind.Local).AddTicks(7881), "Beamer", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_RoomId",
                table: "Inventories",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservations_RoomReservationGuid",
                table: "InventoryReservations",
                column: "RoomReservationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "InventoryReservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomReservations");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
