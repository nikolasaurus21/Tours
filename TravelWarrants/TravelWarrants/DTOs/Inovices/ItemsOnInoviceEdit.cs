namespace TravelWarrants.DTOs.Inovices
{
    public class ItemsOnInoviceEdit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ServiceId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get { return Quantity * Price; } }
        public string NumberOfDays { get; set; }
    }
}
