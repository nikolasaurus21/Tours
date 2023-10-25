namespace TravelWarrants.DTOs.Inovices
{
    public class InvoiceEditDTO
    {
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInvoiceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInvoiceEdit>();
        public List<int>? ItemsToDeleteId { get; set; }
        public bool PriceWithoutVAT { get; set; }
    }
}
