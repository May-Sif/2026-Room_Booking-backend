using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2026, 2, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 9, 10, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2026, 2, 9, 13, 23, 50, 958, DateTimeKind.Utc).AddTicks(227), new DateTime(2026, 2, 9, 22, 23, 50, 957, DateTimeKind.Local).AddTicks(9549), new DateTime(2026, 2, 9, 21, 23, 50, 955, DateTimeKind.Local).AddTicks(4632) });
        }
    }
}
