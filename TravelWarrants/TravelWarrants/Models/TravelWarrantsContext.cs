
namespace TravelWarrants.Models
{
    public class TravelWarrantsContext : DbContext
    {
        public TravelWarrantsContext(DbContextOptions<TravelWarrantsContext> options) : base(options) { }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceService> InvoicesServices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ProformaInvoice> ProformaInvoices { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<CompanyBankAccount>().ToTable("CompanyBankAccounts");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Tour>().ToTable("Tours");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<InvoiceService>().ToTable("InvoicesServices");
            modelBuilder.Entity<ProformaInvoice>().ToTable("ProformaInovices");
            modelBuilder.Entity<UploadedFiles>().ToTable("UploadedFiles");





            // One-to-Many relationship between Inovice and Acount +
            modelBuilder.Entity<Invoice>()
                    .HasMany(f => f.Account)
                    .WithOne(r => r.Invoice)
                    .HasForeignKey(r => r.InvoiceId);


            // One-to-Many relationship between ProformaInovice and Acount +
            modelBuilder.Entity<ProformaInvoice>()
                    .HasMany(f => f.Account)
                    .WithOne(r => r.ProformaInvoice)
                    .HasForeignKey(r => r.ProformaInvoiceId);

            // One-to-Many relationship between Client and Inovice +
            modelBuilder.Entity<Client>()
                    .HasMany(k => k.Inovice)
                    .WithOne(f => f.Client)
                    .HasForeignKey(f => f.ClientId);


            // One-to-Many relationship between Client and ProformaInvoice +
            modelBuilder.Entity<Client>()
                    .HasMany(k => k.ProformaInvoice)
                    .WithOne(f => f.Client)
                    .HasForeignKey(f => f.ClientId);

            // One-to-Many relationship between Company and CompanyGiroAccount +
            modelBuilder.Entity<Company>()
                    .HasMany(f => f.CompanyGiroAccount)
                    .WithOne(f => f.Company)
                    .HasForeignKey(f => f.CompanyId);

            // One-to-Many relathionship between Client and Payment +
            modelBuilder.Entity<Client>()
                    .HasMany(u => u.Payment)
                    .WithOne(k => k.Client)
                    .HasForeignKey(k => k.ClientId);

            // One-to-Many relathionship between Client and Status +
            modelBuilder.Entity<Client>()
                   .HasMany(s => s.Status)
                   .WithOne(k => k.Client)
                   .HasForeignKey(k => k.ClientId);

            // One-to-Many relathionship between Client and Tour +
            modelBuilder.Entity<Client>()
                   .HasMany(t => t.Tour)
                   .WithOne(k => k.Client)
                   .HasForeignKey(k => k.ClientId);

            // One-to-Many relathionship between Client and Account +
            modelBuilder.Entity<Client>()
                   .HasMany(r => r.Account)
                   .WithOne(k => k.Client)
                   .HasForeignKey(k => k.ClientId);

            // One-to-Many relathionship between Vehicle and Tour +
            modelBuilder.Entity<Vehicle>()
                   .HasMany(t => t.Tour)
                   .WithOne(v => v.Vehicle)
                   .HasForeignKey(v => v.VehicleId);

            // One-to-Many relathionship between Inovice and InoviceService +
            modelBuilder.Entity<Invoice>()
                    .HasMany(f => f.InoviceService)
                    .WithOne(uf => uf.Invoice)
                    .HasForeignKey(uf => uf.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many relathionship between ProformaInovice and InoviceService +
            modelBuilder.Entity<ProformaInvoice>()
                    .HasMany(f => f.InvoiceService)
                    .WithOne(uf => uf.ProformaInvoice)
                    .HasForeignKey(uf => uf.ProformaInvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);


            // One-to-Many relathionship between Service and InoviceService +
            modelBuilder.Entity<Service>()
                   .HasMany(uf => uf.InvoiceService)
                   .WithOne(u => u.Service)
                   .HasForeignKey(u => u.ServiceId);

            //One-to-Many relathionship between Drivee and Tour +
            modelBuilder.Entity<Driver>()
                    .HasMany(v => v.Tour)
                    .WithOne(t => t.Driver)
                    .HasForeignKey(t => t.DriverId);

            //One-to-One relationship between Uploadedfiles nad ProformaInvoices
            modelBuilder.Entity<ProformaInvoice>()
                     .HasOne(p => p.UploadedFiles)
                     .WithOne(u => u.ProformaInvoice)
                     .HasForeignKey<ProformaInvoice>(p => p.UploadedFileId)
                     .IsRequired(false);

            modelBuilder.Entity<Account>()
                .Property(a => a.InvoiceId)
                .IsRequired(false);

            modelBuilder.Entity<Account>()
                .Property(a => a.ProformaInvoiceId)
                .IsRequired(false);

            modelBuilder.Entity<InvoiceService>()
            .Property(a => a.InvoiceId)
            .IsRequired(false);

            modelBuilder.Entity<InvoiceService>()
                .Property(a => a.ProformaInvoiceId)
                .IsRequired(false);

        }



    }
}