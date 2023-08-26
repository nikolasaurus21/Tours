namespace TravelWarrants.Models
{
    public class Inovice
    {
        public Inovice()
        {
            this.InoviceService = new HashSet<InoviceService>();
            this.Account = new HashSet<Account>();
        }
        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public int ClientId { get; set; }
        public decimal NoVAT { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public bool Inovice1 { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime CurrencyDate { get; set; }


        public int Service1 { get; set; }
        public int? Service2 { get; set; }
        public int? Service3 { get; set; }
        public int? Service4 { get; set; }
        public int? Service5 { get; set; }


        public string? Description1 { get; set; }
        public string? Description2 { get; set; }
        public string? Description3 { get; set; }
        public string? Description4 { get; set; }
        public string? Description5 { get; set; }


        public decimal Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? Price3 { get; set; }
        public decimal? Price4 { get; set; }
        public decimal? Price5 { get; set; }

        public decimal Value1 { get; set; }
        public decimal? Value2 { get; set; }
        public decimal? Value3 { get; set; }
        public decimal? Value4 { get; set; }
        public decimal? Value5 { get; set; }

        public decimal VAT1 { get; set; }
        public decimal? VAT2 { get; set; }
        public decimal? VAT3 { get; set; }
        public decimal? VAT4 { get; set; }
        public decimal? VAT5 { get; set; }

        
        public string Note { get; set; }
        public bool? PriceWithoutVAT { get; set; }
        public bool? ProinoviceWithoutVAT { get; set; }
        
        public string? Route { get; set; }
        public bool OfferAccepted { get; set; }
        
        public string NumberOfDays { get; set; }

        public virtual Client Client { get; set; }

        public virtual Service Service { get; set; }
        public virtual Service Service6 { get; set; }
        public virtual Service Service7 { get; set; }
        public virtual Service Service8 { get; set; }
        public virtual Service Service9 { get; set; }


        public virtual ICollection<InoviceService> InoviceService { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}
