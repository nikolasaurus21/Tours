using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.DTOs.Proinovce
{
    public class ProinvoiceEditDTO
    {
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInvoiceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInvoiceEdit>();

        public List<int> ItemsToDeleteId { get; set; }
        public bool PriceWithoutVAT { get; set; } = false;
        public bool? ProinoviceWithoutVAT { get; set; }

        public int? RoutePlan { get; set; }
        public bool OfferAccepted { get; set; }
    }
}
