namespace TravelWarrants.DTOs.Proinovce
{
    public class ProinvoiceGetDTO
    {
        public int Id { get; set; }
        public bool OfferAccepted { get; set; }
        public string Number { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
