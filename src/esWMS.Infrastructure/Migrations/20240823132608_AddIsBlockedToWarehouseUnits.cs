using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBlockedToWarehouseUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "WarehouseUnits",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "WarehouseUnits");
        }
    }
}
