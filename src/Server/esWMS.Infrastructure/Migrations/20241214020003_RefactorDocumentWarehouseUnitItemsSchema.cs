using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDocumentWarehouseUnitItemsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DocumentWarehouseUnitItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems",
                column: "DocumentItemId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentWarehouseUnitItems",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.AddColumn<string>(
                name: "DocumentWarehouseUnitItemId",
                table: "DocumentWarehouseUnitItems",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql("UPDATE DocumentWarehouseUnitItems SET DocumentWarehouseUnitItemId = UUID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentWarehouseUnitItems",
                table: "DocumentWarehouseUnitItems",
                column: "DocumentWarehouseUnitItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentWarehouseUnitItems",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.DropColumn(
                name: "DocumentWarehouseUnitItemId",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentWarehouseUnitItems",
                table: "DocumentWarehouseUnitItems",
                columns: new[] { "DocumentItemId", "WarehouseUnitItemId" });

            migrationBuilder.DropIndex(
                name: "IX_DocumentWarehouseUnitItems_DocumentItemId",
                table: "DocumentWarehouseUnitItems");
        }
    }
}
