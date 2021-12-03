using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Weeks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "This swish uptown hotel on the Upper East side is near the Met, Central Park and lots of upscale shopping in Fifth Avenue.", "The Mark, New York" },
                    { 2, "Right on La Croisette, this gleaming white, art deco icon, plays host to many a celebrity-filled shindig during the Cannes Film Festival.", "Hotel Martinez, Cannes" },
                    { 3, "On its own private, man-made island, with a lovely white sand beach, the resort is accessed by a 300-metre bridge from the mainland.", "BVLGARI Resort & Residences, Dubai" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "kalle@gmail.com", "Kalle", "Anka", "hej123" },
                    { 2, "fredric@gmail.com", "Fredric", "Fredriksson", "hej123" },
                    { 3, "anders@gmail.com", "Anders", "Andersson", "hej123" },
                    { 4, "maria@gmail.com", "Maria", "Svensson", "hej123" },
                    { 5, "julia@gmail.com", "Julia", "Larsson", "hej123" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "HotelId", "Price", "Size" },
                values: new object[,]
                {
                    { 1, 1, 1000, "Singelrum" },
                    { 2, 1, 2000, "Dubbelrum" },
                    { 3, 1, 3000, "Familjerum" },
                    { 4, 2, 1000, "Singelrum" },
                    { 5, 2, 2000, "Dubbelrum" },
                    { 6, 2, 3000, "Familjerum" },
                    { 7, 3, 1000, "Singelrum" },
                    { 8, 3, 2000, "Dubbelrum" },
                    { 9, 3, 3000, "Familjerum" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "EndDate", "RoomId", "StartDate", "TotalPrice", "UserId", "Weeks" },
                values: new object[] { 1, new DateTime(2021, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000, 1, 1 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "EndDate", "RoomId", "StartDate", "TotalPrice", "UserId", "Weeks" },
                values: new object[] { 2, new DateTime(2021, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000, 1, 2 });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "EndDate", "RoomId", "StartDate", "TotalPrice", "UserId", "Weeks" },
                values: new object[] { 3, new DateTime(2021, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 21000, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
