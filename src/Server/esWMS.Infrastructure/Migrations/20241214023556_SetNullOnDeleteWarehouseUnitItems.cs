using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetNullOnDeleteWarehouseUnitItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentWarehouseUnitItems_WarehouseUnitItems_WarehouseUnitI~",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseUnitItemId",
                table: "DocumentWarehouseUnitItems",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentWarehouseUnitItems_WarehouseUnitItems_WarehouseUnitI~",
                table: "DocumentWarehouseUnitItems",
                column: "WarehouseUnitItemId",
                principalTable: "WarehouseUnitItems",
                principalColumn: "WarehouseUnitItemId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentWarehouseUnitItems_WarehouseUnitItems_WarehouseUnitI~",
                table: "DocumentWarehouseUnitItems");

            migrationBuilder.UpdateData(
                table: "DocumentWarehouseUnitItems",
                keyColumn: "WarehouseUnitItemId",
                keyValue: null,
                column: "WarehouseUnitItemId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseUnitItemId",
                table: "DocumentWarehouseUnitItems",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentWarehouseUnitItems_WarehouseUnitItems_WarehouseUnitI~",
                table: "DocumentWarehouseUnitItems",
                column: "WarehouseUnitItemId",
                principalTable: "WarehouseUnitItems",
                principalColumn: "WarehouseUnitItemId");
        }
    }
}
