using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrikaYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaterialRequestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "MaterialRequests");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MaterialRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "MaterialRequests");

            migrationBuilder.AddColumn<bool>(
                name: "RequestStatus",
                table: "MaterialRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
