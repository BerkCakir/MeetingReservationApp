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

            migrationBuilder.CreateTable(
                name: "RoomReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomReservationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MeetingStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttendantCount = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservations", x => x.Id);
                    table.UniqueConstraint("AK_RoomReservations_RoomReservationGuid", x => x.RoomReservationGuid);
                    table.ForeignKey(
                        name: "FK_RoomReservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
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
                        name: "FK_InventoryReservations_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryReservations_RoomReservations_RoomReservationGuid",
                        column: x => x.RoomReservationGuid,
                        principalTable: "RoomReservations",
                        principalColumn: "RoomReservationGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "OfficeEndHours", "OfficeEndMinutes", "OfficeStartHours", "OfficeStartMinutes" },
                values: new object[] { 1, new DateTime(2021, 10, 10, 1, 7, 28, 479, DateTimeKind.Local).AddTicks(2897), new DateTime(2021, 10, 10, 1, 7, 28, 479, DateTimeKind.Local).AddTicks(3283), "Amsterdam", 17, 0, 8, 30 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "OfficeEndHours", "OfficeEndMinutes", "OfficeStartHours", "OfficeStartMinutes" },
                values: new object[] { 2, new DateTime(2021, 10, 10, 1, 7, 28, 479, DateTimeKind.Local).AddTicks(3655), new DateTime(2021, 10, 10, 1, 7, 28, 479, DateTimeKind.Local).AddTicks(3657), "Berlin", 20, 0, 8, 30 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AttendanceCapacity", "CreatedDate", "HasChairs", "LocationId", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4866), true, 1, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4886), "Meeting Room 1" },
                    { 2, 12, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4899), true, 1, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4900), "Meeting Room 2" },
                    { 3, 5, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4905), false, 1, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4906), "Meeting Room 3" },
                    { 4, 20, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4911), true, 2, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4912), "Meeting Room A" },
                    { 5, 5, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4916), false, 2, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4918), "Meeting Room B" },
                    { 6, 25, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4922), true, 2, new DateTime(2021, 10, 10, 1, 7, 28, 485, DateTimeKind.Local).AddTicks(4923), "Meeting Room C" }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedDate", "InventoryPurpose", "IsFixed", "ModifiedDate", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2619), 2, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2636), "Beamer", 1 },
                    { 2, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2648), 1, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2650), "WhiteBoard", 2 },
                    { 3, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2654), 2, false, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2656), "Television", 3 },
                    { 4, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2660), 1, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2661), "WhiteBoard", 3 },
                    { 5, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2666), 3, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2667), "Video Conference Equipment", 4 },
                    { 6, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2671), 2, false, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2672), "Television", 5 },
                    { 7, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2677), 3, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2678), "Television", 5 },
                    { 8, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2682), 2, true, new DateTime(2021, 10, 10, 1, 7, 28, 488, DateTimeKind.Local).AddTicks(2684), "Beamer", 6 }
                });

            migrationBuilder.InsertData(
                table: "RoomReservations",
                columns: new[] { "Id", "AttendantCount", "CreatedDate", "Description", "MeetingEndTime", "MeetingStartTime", "ModifiedDate", "RoomId", "RoomReservationGuid" },
                values: new object[] { 1, 7, new DateTime(2021, 10, 10, 1, 7, 28, 491, DateTimeKind.Local).AddTicks(174), "Marketing Unit Meeting", new DateTime(2021, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 10, 1, 7, 28, 491, DateTimeKind.Local).AddTicks(192), 2, new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529") });

            migrationBuilder.InsertData(
                table: "InventoryReservations",
                columns: new[] { "Id", "CreatedDate", "InventoryId", "ModifiedDate", "RoomReservationGuid" },
                values: new object[] { 1, new DateTime(2021, 10, 10, 1, 7, 28, 498, DateTimeKind.Local).AddTicks(4416), 2, new DateTime(2021, 10, 10, 1, 7, 28, 498, DateTimeKind.Local).AddTicks(4435), new Guid("019ee26d-5111-4d96-abfa-5bd6c966d529") });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_RoomId",
                table: "Inventories",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservations_InventoryId",
                table: "InventoryReservations",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservations_RoomReservationGuid",
                table: "InventoryReservations",
                column: "RoomReservationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_RoomReservations_RoomId",
                table: "RoomReservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryReservations");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "RoomReservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
