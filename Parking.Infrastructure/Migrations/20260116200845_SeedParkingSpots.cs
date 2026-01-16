using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedParkingSpots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkingSpots",
                columns: new[] { "Id", "Code", "IsOccupied", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "M-01", false, 1 },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "M-02", false, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222221"), "C-01", false, 2 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "C-02", false, 2 },
                    { new Guid("33333333-3333-3333-3333-333333333331"), "T-01", false, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"));

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "ParkingSpots",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"));
        }
    }
}
