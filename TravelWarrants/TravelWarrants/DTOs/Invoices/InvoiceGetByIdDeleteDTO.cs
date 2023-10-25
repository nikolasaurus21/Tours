namespace TravelWarrants.DTOs.Inovices
{
    public class InvoiceGetByIdDeleteDTO
    {
        public string Number { get; set; }

        public string Date { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
    }
}
