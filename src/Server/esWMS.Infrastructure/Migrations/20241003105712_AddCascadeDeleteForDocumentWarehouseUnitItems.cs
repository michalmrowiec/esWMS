using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForDocumentWarehouseUnitItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentWarehouseUnitItems_DocumentItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentWarehouseUnitItems_DocumentItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems",
                column: "DocumentItemId",
                principalTable: "DocumentItems",
                principalColumn: "DocumentItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentWarehouseUnitItems_DocumentItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentWarehouseUnitItems_DocumentItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems",
                column: "DocumentItemId",
                principalTable: "DocumentItems",
                principalColumn: "DocumentItemId");
        }
    }
}
