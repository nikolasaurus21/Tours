using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWarrants.Models
{
    public class InvoiceService
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int ServiceId { get; set; }
        public int? ProformaInvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
        public decimal VAT { get; set; }
        public string NumberOfDays { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Service Service { get; set; }

        public virtual ProformaInvoice ProformaInvoice { get; set; }
    }
}
