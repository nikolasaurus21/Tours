using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelWarrants.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFiledInovice1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inovice1",
                table: "Inovices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Inovice1",
                table: "Inovices",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
