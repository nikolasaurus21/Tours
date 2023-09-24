namespace TravelWarrants.DTOs.Inovices
{
    public class InoviceEditDTO
    {
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInoviceEdit> ItemsOnInovice { get; set; } = new List<ItemsOnInoviceEdit>();
        public List<int> ItemsToDeleteId { get; set; }
        public bool PriceWithoutVAT { get; set; }
    }
}
