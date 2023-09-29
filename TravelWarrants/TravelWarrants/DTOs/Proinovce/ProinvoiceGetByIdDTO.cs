using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.DTOs.Proinovce
{
    public class ProinvoiceGetByIdDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }
        public List<ItemsOnInoviceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInoviceEdit>();
        public IFormFile? RoutePlan { get; set; }
        public bool OfferAccepted { get; set; }
        public bool PriceWithoutVAT { get; set; }
        public string Number {  get; set; }
        public bool ProinvoiceWithoutVAT { get; set; }
        public string FileName {  get; set; }
    }
}
