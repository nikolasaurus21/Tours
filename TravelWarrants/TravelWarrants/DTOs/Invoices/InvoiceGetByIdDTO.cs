namespace TravelWarrants.DTOs.Inovices
{
    public class InvoiceGetByIdDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Number { get; set; }
        public List<ItemsOnInvoiceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInvoiceEdit>();

        public bool PriceWithoutVAT { get; set; }
    }
}
