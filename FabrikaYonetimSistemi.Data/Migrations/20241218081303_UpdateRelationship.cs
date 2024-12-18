using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabrikaYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Storages_StorageId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_Materials_MaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_Personnels_PersonelId",
                table: "MaterialTransactions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialTransactions_MaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Materials_StorageId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "MaterialTransactions",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "PersonelId",
                table: "MaterialTransactions",
                newName: "StorageMaterialId");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "MaterialTransactions",
                newName: "QuantityChange");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "MaterialTransactions",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "MaterialTransactions",
                newName: "PersonnelId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialTransactions_PersonelId",
                table: "MaterialTransactions",
                newName: "IX_MaterialTransactions_StorageMaterialId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Factories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "StorageMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageMaterial_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactions_PersonnelId",
                table: "MaterialTransactions",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageMaterial_MaterialId",
                table: "StorageMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageMaterial_StorageId",
                table: "StorageMaterial",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_Personnels_PersonnelId",
                table: "MaterialTransactions",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_StorageMaterial_StorageMaterialId",
                table: "MaterialTransactions",
                column: "StorageMaterialId",
                principalTable: "StorageMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_Personnels_PersonnelId",
                table: "MaterialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialTransactions_StorageMaterial_StorageMaterialId",
                table: "MaterialTransactions");

            migrationBuilder.DropTable(
                name: "StorageMaterial");

            migrationBuilder.DropIndex(
                name: "IX_MaterialTransactions_PersonnelId",
                table: "MaterialTransactions");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "MaterialTransactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "MaterialTransactions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "StorageMaterialId",
                table: "MaterialTransactions",
                newName: "PersonelId");

            migrationBuilder.RenameColumn(
                name: "QuantityChange",
                table: "MaterialTransactions",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "PersonnelId",
                table: "MaterialTransactions",
                newName: "Action");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialTransactions_StorageMaterialId",
                table: "MaterialTransactions",
                newName: "IX_MaterialTransactions_PersonelId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Factories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTransactions_MaterialId",
                table: "MaterialTransactions",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_StorageId",
                table: "Materials",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Storages_StorageId",
                table: "Materials",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_Materials_MaterialId",
                table: "MaterialTransactions",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialTransactions_Personnels_PersonelId",
                table: "MaterialTransactions",
                column: "PersonelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
