using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrikaYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestMaterialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RequestStatus = table.Column<bool>(type: "bit", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StorageMaterialId = table.Column<int>(type: "int", nullable: false),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_AspNetUsers_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialRequests_StorageMaterial_StorageMaterialId",
                        column: x => x.StorageMaterialId,
                        principalTable: "StorageMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_PersonnelId",
                table: "MaterialRequests",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequests_StorageMaterialId",
                table: "MaterialRequests",
                column: "StorageMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialRequests");
        }
    }
}
