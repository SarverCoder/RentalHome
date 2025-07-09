using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowsPets",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsFurnished",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowsPets",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFurnished",
                table: "Properties",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
