namespace TravelWarrants.Models
{
    public class InoviceService
    {
        public int Id { get; set; }
        public int InoviceId { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }
        public decimal VAT { get; set; }
        public string NumberOfDays { get; set; }
        public virtual Inovice Inovice { get; set; }
        public virtual Service Service { get; set; }
    }
}
