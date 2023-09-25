using System.ComponentModel.DataAnnotations;

namespace TravelWarrants.DTOs.Inovices
{
    public class InoviceNewDTO
    {
        
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInovice> ItemsOnInovice { get; set; } = new List<ItemsOnInovice>();

        public bool PriceWithoutVAT { get; set; } = false;
        // public bool? ProinoviceWithoutVAT { get; set; }

        // public string? Route { get; set; }
        //public bool OfferAccepted { get; set; }




    }
}
