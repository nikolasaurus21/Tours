using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TravelWarrants.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PlaceName = table.Column<string>(type: "text", nullable: false),
                    RegistrationNUmber = table.Column<string>(type: "text", nullable: false),
                    VAT = table.Column<decimal>(type: "numeric", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Fax = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Excursion = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PTT = table.Column<string>(type: "text", nullable: false),
                    Place = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Fax = table.Column<string>(type: "text", nullable: false),
                    MobilePhone = table.Column<string>(type: "text", nullable: false),
                    TIN = table.Column<string>(type: "text", nullable: false),
                    VAT = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NUmberOfPhone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    VATRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Registration = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    NumberOfSeats = table.Column<string>(type: "text", nullable: false),
                    FuelConsumption = table.Column<string>(type: "text", nullable: false),
                    Mileage = table.Column<decimal>(type: "numeric", nullable: false),
                    Change = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    AmountOfAccount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountOfDeposit = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statuses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesGiroAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesGiroAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompaniesGiroAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inovices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    NoVAT = table.Column<decimal>(type: "numeric", nullable: false),
                    VAT = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Inovice1 = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrencyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Service1 = table.Column<int>(type: "integer", nullable: false),
                    Service2 = table.Column<int>(type: "integer", nullable: true),
                    Service3 = table.Column<int>(type: "integer", nullable: true),
                    Service4 = table.Column<int>(type: "integer", nullable: true),
                    Service5 = table.Column<int>(type: "integer", nullable: true),
                    Description1 = table.Column<string>(type: "text", nullable: true),
                    Description2 = table.Column<string>(type: "text", nullable: true),
                    Description3 = table.Column<string>(type: "text", nullable: true),
                    Description4 = table.Column<string>(type: "text", nullable: true),
                    Description5 = table.Column<string>(type: "text", nullable: true),
                    Price1 = table.Column<decimal>(type: "numeric", nullable: false),
                    Price2 = table.Column<decimal>(type: "numeric", nullable: true),
                    Price3 = table.Column<decimal>(type: "numeric", nullable: true),
                    Price4 = table.Column<decimal>(type: "numeric", nullable: true),
                    Price5 = table.Column<decimal>(type: "numeric", nullable: true),
                    Value1 = table.Column<decimal>(type: "numeric", nullable: false),
                    Value2 = table.Column<decimal>(type: "numeric", nullable: true),
                    Value3 = table.Column<decimal>(type: "numeric", nullable: true),
                    Value4 = table.Column<decimal>(type: "numeric", nullable: true),
                    Value5 = table.Column<decimal>(type: "numeric", nullable: true),
                    VAT1 = table.Column<decimal>(type: "numeric", nullable: false),
                    VAT2 = table.Column<decimal>(type: "numeric", nullable: true),
                    VAT3 = table.Column<decimal>(type: "numeric", nullable: true),
                    VAT4 = table.Column<decimal>(type: "numeric", nullable: true),
                    VAT5 = table.Column<decimal>(type: "numeric", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: false),
                    PriceWithoutVAT = table.Column<bool>(type: "boolean", nullable: true),
                    ProinoviceWithoutVAT = table.Column<bool>(type: "boolean", nullable: true),
                    Route = table.Column<string>(type: "text", nullable: true),
                    OfferAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    NumberOfDays = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inovices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inovices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inovices_Services_Service1",
                        column: x => x.Service1,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inovices_Services_Service2",
                        column: x => x.Service2,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inovices_Services_Service3",
                        column: x => x.Service3,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inovices_Services_Service4",
                        column: x => x.Service4,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inovices_Services_Service5",
                        column: x => x.Service5,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Departure = table.Column<string>(type: "text", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false),
                    Mileage = table.Column<decimal>(type: "numeric", nullable: false),
                    NumberOfPassengers = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Toll = table.Column<decimal>(type: "numeric", nullable: false),
                    Fuel = table.Column<decimal>(type: "numeric", nullable: false),
                    TimeOfTour = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    DepartureLatitude = table.Column<decimal>(type: "numeric", nullable: true),
                    DepartureLongitude = table.Column<decimal>(type: "numeric", nullable: true),
                    DestinationLatitude = table.Column<decimal>(type: "numeric", nullable: true),
                    DestinationLongitude = table.Column<decimal>(type: "numeric", nullable: true),
                    StartMileage = table.Column<decimal>(type: "numeric", nullable: false),
                    EndMileage = table.Column<decimal>(type: "numeric", nullable: false),
                    VehicleMileage = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    DriverId = table.Column<int>(type: "integer", nullable: false),
                    FuelPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    NumberOfDays = table.Column<decimal>(type: "numeric", nullable: true),
                    IntermediateDestinations = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InoviceId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Inovices_InoviceId",
                        column: x => x.InoviceId,
                        principalTable: "Inovices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InovicesServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InoviceId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    VAT = table.Column<decimal>(type: "numeric", nullable: false),
                    NumberOfDays = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InovicesServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InovicesServices_Inovices_InoviceId",
                        column: x => x.InoviceId,
                        principalTable: "Inovices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InovicesServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InoviceId",
                table: "Accounts",
                column: "InoviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesGiroAccounts_CompanyId",
                table: "CompaniesGiroAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inovices_ClientId",
                table: "Inovices",
                column: "ClientId");

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

            migrationBuilder.CreateIndex(
                name: "IX_InovicesServices_InoviceId",
                table: "InovicesServices",
                column: "InoviceId");

            migrationBuilder.CreateIndex(
                name: "IX_InovicesServices_ServiceId",
                table: "InovicesServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_ClientId",
                table: "Statuses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ClientId",
                table: "Tours",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_DriverId",
                table: "Tours",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_VehicleId",
                table: "Tours",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CompaniesGiroAccounts");

            migrationBuilder.DropTable(
                name: "InovicesServices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Inovices");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
