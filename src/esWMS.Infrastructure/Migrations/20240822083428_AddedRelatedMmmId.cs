using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esWMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelatedMmmId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "WarehouseUnitItems",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RelatedMmmId",
                table: "Documents",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RelatedMmmId",
                table: "Documents",
                column: "RelatedMmmId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Documents_RelatedMmmId",
                table: "Documents",
                column: "RelatedMmmId",
                principalTable: "Documents",
                principalColumn: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Documents_RelatedMmmId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_RelatedMmmId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "WarehouseUnitItems");

            migrationBuilder.DropColumn(
                name: "RelatedMmmId",
                table: "Documents");
        }
    }
}
