namespace TravelWarrants.Models
{
    public class ProformaInvoice
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public int ClientId { get; set; }
        public decimal NoVAT { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime CurrencyDate { get; set; }
        public string Note { get; set; }
        public bool? PriceWithoutVAT { get; set; }
        public bool? ProinoviceWithoutVAT { get; set; }
        public string? Route { get; set; }
        public bool? OfferAccepted { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<InoviceService> InoviceService { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}
