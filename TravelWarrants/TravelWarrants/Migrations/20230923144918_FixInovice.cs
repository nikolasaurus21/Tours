using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelWarrants.Migrations
{
    /// <inheritdoc />
    public partial class FixInovice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Inovices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumberOfDays",
                table: "Inovices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
