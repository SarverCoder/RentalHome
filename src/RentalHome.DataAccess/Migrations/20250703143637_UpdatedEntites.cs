using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RentalHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_PropertyTypes_PreferredPropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_PropertyTypes_PropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PreferredPropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PreferredPropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PropertyTypeId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MinRentalPeriod",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyTypeId",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredPropertyTypeId",
                table: "Tenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyTypeId",
                table: "Tenants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinRentalPeriod",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyTypeId",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PreferredPropertyTypeId",
                table: "Tenants",
                column: "PreferredPropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyTypeId",
                table: "Tenants",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_PropertyTypes_PreferredPropertyTypeId",
                table: "Tenants",
                column: "PreferredPropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_PropertyTypes_PropertyTypeId",
                table: "Tenants",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id");
        }
    }
}
