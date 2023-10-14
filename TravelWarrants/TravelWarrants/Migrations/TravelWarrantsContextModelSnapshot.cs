﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelWarrants.Models;

#nullable disable

namespace TravelWarrants.Migrations
{
    [DbContext(typeof(TravelWarrantsContext))]
    partial class TravelWarrantsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TravelWarrants.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("ClientId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("InoviceId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProformaInvoiceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("InoviceId");

                    b.HasIndex("ProformaInvoiceId");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("Excursion")
                        .HasColumnType("boolean");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegistrationNUmber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("VAT")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PTT")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TIN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VAT")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.CompanyGiroAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompaniesGiroAccounts", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("NUmberOfPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Inovice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CurrencyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("NoVAT")
                        .HasColumnType("numeric");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<bool?>("PriceWithoutVAT")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VAT")
                        .HasColumnType("numeric");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Inovices", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.InoviceService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InoviceId")
                        .HasColumnType("integer");

                    b.Property<string>("NumberOfDays")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("ProformaInvoiceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("VAT")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("InoviceId");

                    b.HasIndex("ProformaInvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("InovicesServices", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Places", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.ProformaInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CurrencyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("NoVAT")
                        .HasColumnType("numeric");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<bool?>("OfferAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("PriceWithoutVAT")
                        .HasColumnType("boolean");

                    b.Property<bool?>("ProinoviceWithoutVAT")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<int?>("UploadedFileId")
                        .HasColumnType("integer");

                    b.Property<decimal>("VAT")
                        .HasColumnType("numeric");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("UploadedFileId")
                        .IsUnique();

                    b.ToTable("ProformaInovices", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("VATRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Services", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountOfAccount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AmountOfDeposit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Statuses", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Departure")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("DepartureLatitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("DepartureLongitude")
                        .HasColumnType("numeric");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("DestinationLatitude")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("DestinationLongitude")
                        .HasColumnType("numeric");

                    b.Property<int?>("DriverId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<decimal>("EndMileage")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Fuel")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("FuelPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("IntermediateDestinations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Mileage")
                        .HasColumnType("numeric");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("NumberOfDays")
                        .HasColumnType("numeric");

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("StartMileage")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("TimeOfTour")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Toll")
                        .HasColumnType("numeric");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.Property<decimal>("VehicleMileage")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Tours", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.UploadedFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("UploadedFiles", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Change")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FuelConsumption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Mileage")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumberOfSeats")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("TravelWarrants.Models.Account", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("Account")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelWarrants.Models.Inovice", "Inovice")
                        .WithMany("Account")
                        .HasForeignKey("InoviceId");

                    b.HasOne("TravelWarrants.Models.ProformaInvoice", "ProformaInvoice")
                        .WithMany("Account")
                        .HasForeignKey("ProformaInvoiceId");

                    b.Navigation("Client");

                    b.Navigation("Inovice");

                    b.Navigation("ProformaInvoice");
                });

            modelBuilder.Entity("TravelWarrants.Models.CompanyGiroAccount", b =>
                {
                    b.HasOne("TravelWarrants.Models.Company", "Company")
                        .WithMany("CompanyGiroAccount")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TravelWarrants.Models.Inovice", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("Inovice")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("TravelWarrants.Models.InoviceService", b =>
                {
                    b.HasOne("TravelWarrants.Models.Inovice", "Inovice")
                        .WithMany("InoviceService")
                        .HasForeignKey("InoviceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TravelWarrants.Models.ProformaInvoice", "ProformaInvoice")
                        .WithMany("InoviceService")
                        .HasForeignKey("ProformaInvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TravelWarrants.Models.Service", "Service")
                        .WithMany("InoviceService")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inovice");

                    b.Navigation("ProformaInvoice");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("TravelWarrants.Models.Payment", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("Payment")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("TravelWarrants.Models.ProformaInvoice", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("ProformaInvoice")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelWarrants.Models.UploadedFiles", "UploadedFiles")
                        .WithOne("ProformaInvoice")
                        .HasForeignKey("TravelWarrants.Models.ProformaInvoice", "UploadedFileId");

                    b.Navigation("Client");

                    b.Navigation("UploadedFiles");
                });

            modelBuilder.Entity("TravelWarrants.Models.Status", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("Status")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("TravelWarrants.Models.Tour", b =>
                {
                    b.HasOne("TravelWarrants.Models.Client", "Client")
                        .WithMany("Tour")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelWarrants.Models.Driver", "Driver")
                        .WithMany("Tour")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelWarrants.Models.Vehicle", "Vehicle")
                        .WithMany("Tour")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Driver");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TravelWarrants.Models.Client", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Inovice");

                    b.Navigation("Payment");

                    b.Navigation("ProformaInvoice");

                    b.Navigation("Status");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelWarrants.Models.Company", b =>
                {
                    b.Navigation("CompanyGiroAccount");
                });

            modelBuilder.Entity("TravelWarrants.Models.Driver", b =>
                {
                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelWarrants.Models.Inovice", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("InoviceService");
                });

            modelBuilder.Entity("TravelWarrants.Models.ProformaInvoice", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("InoviceService");
                });

            modelBuilder.Entity("TravelWarrants.Models.Service", b =>
                {
                    b.Navigation("InoviceService");
                });

            modelBuilder.Entity("TravelWarrants.Models.UploadedFiles", b =>
                {
                    b.Navigation("ProformaInvoice")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelWarrants.Models.Vehicle", b =>
                {
                    b.Navigation("Tour");
                });
#pragma warning restore 612, 618
        }
    }
}
