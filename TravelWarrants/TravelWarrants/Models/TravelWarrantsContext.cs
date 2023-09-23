
namespace TravelWarrants.Models
{
    public class TravelWarrantsContext : DbContext
    {
        public TravelWarrantsContext(DbContextOptions<TravelWarrantsContext> options) : base(options) { }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyGiroAccount> CompaniesGiroAccounts { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Inovice> Inovices { get; set; }
        public DbSet<InoviceService> InovicesServices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<CompanyGiroAccount>().ToTable("CompaniesGiroAccounts");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Inovice>().ToTable("Inovices");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Place>().ToTable("Places");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Tour>().ToTable("Tours");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<InoviceService>().ToTable("InovicesServices");



            //modelBuilder.Entity<Account>()
            //    .Property(x => x.InoviceId)
            //    .IsRequired(false);

            // One-to-Many relationship between Inovice and Acount +
            modelBuilder.Entity<Inovice>()
                    .HasMany(f => f.Account)
                    .WithOne(r => r.Inovice)
                    .HasForeignKey(r => r.InoviceId);

            // One-to-Many relationship between Client and Inovice +
            modelBuilder.Entity<Client>()
                    .HasMany(k => k.Inovice)
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
            modelBuilder.Entity<Inovice>()
                    .HasMany(f => f.InoviceService)
                    .WithOne(uf => uf.Inovice)
                    .HasForeignKey(uf => uf.InoviceId)
                    .OnDelete(DeleteBehavior.Restrict);


            // One-to-Many relathionship between Service and InoviceService +
            modelBuilder.Entity<Service>()
                   .HasMany(uf => uf.InoviceService)
                   .WithOne(u => u.Service)
                   .HasForeignKey(u => u.ServiceId);

            //One-to-Many relathionship between Drivee and Tour +
            modelBuilder.Entity<Driver>()
                    .HasMany(v => v.Tour)
                    .WithOne(t => t.Driver)
                    .HasForeignKey(t => t.DriverId);

           
        }



    }
}