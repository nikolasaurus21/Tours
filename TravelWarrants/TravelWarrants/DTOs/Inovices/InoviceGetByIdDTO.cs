namespace TravelWarrants.DTOs.Inovices
{
    public class InoviceGetByIdDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Number {  get; set; }
        public List<ItemsOnInoviceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInoviceEdit>();
        
        public bool PriceWithoutVAT { get; set; }
    }
}
