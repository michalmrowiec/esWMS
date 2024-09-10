using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIsMediaOfWarehouseUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseUnits_Products_MediaId",
                table: "WarehouseUnits");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseUnits_MediaId",
                table: "WarehouseUnits");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "WarehouseUnits");

            migrationBuilder.AddColumn<bool>(
                name: "IsMediaOfWarehouseUnit",
                table: "WarehouseUnitItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMediaOfWarehouseUnit",
                table: "WarehouseUnitItems");

            migrationBuilder.AddColumn<string>(
                name: "MediaId",
                table: "WarehouseUnits",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseUnits_MediaId",
                table: "WarehouseUnits",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseUnits_Products_MediaId",
                table: "WarehouseUnits",
                column: "MediaId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
