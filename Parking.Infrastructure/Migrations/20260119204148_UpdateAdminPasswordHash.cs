using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2b$10$pvVtvha0LggnboHgBUA6J.kWPgprV2QsIBeXuhSI0hplO5mSeWJGa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2a$11$8Y0eZyZxH8f1YFz1mZJw5O0G9fC7Zb8F8yF5xJ6u7n9yKZc2e6H9e");
        }
    }
}
