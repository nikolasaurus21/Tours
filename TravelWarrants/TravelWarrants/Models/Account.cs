using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWarrants.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProformaInvoiceId { get; set; }
        public DateTime? Date { get; set; }
        public int? ClientId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Amount { get; set; }
        public string Note { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Client Client { get; set; }
        public virtual ProformaInvoice ProformaInvoice { get; set; }
    }
}
