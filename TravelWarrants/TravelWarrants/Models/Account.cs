namespace TravelWarrants.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int? InoviceId { get; set; }
        public int? ProformaInvoiceId { get; set; }
        public DateTime? Date { get; set; }
        public int? ClientId { get; set; }
        public decimal? Amount { get; set; }
        public string Note { get; set; }
        public virtual Inovice Inovice { get; set; }
        public virtual Client Client { get; set; }
        public virtual ProformaInvoice ProformaInvoice { get; set; }
    }
}
