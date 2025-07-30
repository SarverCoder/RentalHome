using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LandlordEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Landlords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 17, 44, 948, DateTimeKind.Utc).AddTicks(3760));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Landlords",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 16, 20, 40, 12, 922, DateTimeKind.Utc).AddTicks(5142));
        }
    }
}
