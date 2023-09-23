namespace TravelWarrants.DTOs
{
    public class InoviceSaveDTO
    {
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInovice> ItemsOnInovice { get; set; } = new List<ItemsOnInovice>();
   
        public bool PriceWithoutVAT { get; set; }
       // public bool? ProinoviceWithoutVAT { get; set; }

       // public string? Route { get; set; }
        //public bool OfferAccepted { get; set; }

        

      
    }
}
