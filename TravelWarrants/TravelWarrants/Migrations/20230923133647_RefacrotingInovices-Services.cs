using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelWarrants.Migrations
{
    /// <inheritdoc />
    public partial class RefacrotingInovicesServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inovices_Services_Service1",
                table: "Inovices");

            migrationBuilder.DropForeignKey(
                name: "FK_Inovices_Services_Service2",
                table: "Inovices");

            migrationBuilder.DropForeignKey(
                name: "FK_Inovices_Services_Service3",
                table: "Inovices");

            migrationBuilder.DropForeignKey(
                name: "FK_Inovices_Services_Service4",
                table: "Inovices");

            migrationBuilder.DropForeignKey(
                name: "FK_Inovices_Services_Service5",
                table: "Inovices");

            migrationBuilder.DropIndex(
                name: "IX_Inovices_Service1",
                table: "Inovices");

            migrationBuilder.DropIndex(
                name: "IX_Inovices_Service2",
                table: "Inovices");

            migrationBuilder.DropIndex(
                name: "IX_Inovices_Service3",
                table: "Inovices");

            migrationBuilder.DropIndex(
                name: "IX_Inovices_Service4",
                table: "Inovices");

            migrationBuilder.DropIndex(
                name: "IX_Inovices_Service5",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Description1",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Description3",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Description4",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Description5",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Price1",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Price2",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Price3",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Price4",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Price5",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Service1",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Service2",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Service3",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Service4",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Service5",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "VAT1",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "VAT2",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "VAT3",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "VAT4",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "VAT5",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Value1",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Value2",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Value3",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Value4",
                table: "Inovices");

            migrationBuilder.DropColumn(
                name: "Value5",
                table: "Inovices");

            migrationBuilder.AlterColumn<bool>(
                name: "OfferAccepted",
                table: "Inovices",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OfferAccepted",
                table: "Inovices",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description1",
                table: "Inovices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Inovices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description3",
                table: "Inovices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description4",
                table: "Inovices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description5",
                table: "Inovices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price1",
                table: "Inovices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price2",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price3",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price4",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price5",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service1",
                table: "Inovices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Service2",
                table: "Inovices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service3",
                table: "Inovices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service4",
                table: "Inovices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service5",
                table: "Inovices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT1",
                table: "Inovices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT2",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT3",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT4",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT5",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value1",
                table: "Inovices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Value2",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value3",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value4",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value5",
                table: "Inovices",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_Service1",
                table: "Inovices",
                column: "Service1");

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_Service2",
                table: "Inovices",
                column: "Service2");

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_Service3",
                table: "Inovices",
                column: "Service3");

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_Service4",
                table: "Inovices",
                column: "Service4");

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_Service5",
                table: "Inovices",
                column: "Service5");

            migrationBuilder.AddForeignKey(
                name: "FK_Inovices_Services_Service1",
                table: "Inovices",
                column: "Service1",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inovices_Services_Service2",
                table: "Inovices",
                column: "Service2",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inovices_Services_Service3",
                table: "Inovices",
                column: "Service3",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inovices_Services_Service4",
                table: "Inovices",
                column: "Service4",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inovices_Services_Service5",
                table: "Inovices",
                column: "Service5",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
