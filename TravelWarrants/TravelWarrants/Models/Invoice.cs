using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWarrants.Models
{
    public class Invoice
    {

        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public int ClientId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal NoVAT { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VAT { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime CurrencyDate { get; set; }
        public string Note { get; set; }
        public bool? PriceWithoutVAT { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<InvoiceService> InoviceService { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}
