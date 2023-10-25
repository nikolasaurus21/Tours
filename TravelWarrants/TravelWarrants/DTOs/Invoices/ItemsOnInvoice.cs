namespace TravelWarrants.DTOs.Inovices
{
    public class ItemsOnInvoice
    {

        public string Description { get; set; }

        public int ServiceId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string NumberOfDays { get; set; }
    }
}
