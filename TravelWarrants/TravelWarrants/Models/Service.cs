namespace TravelWarrants.Models
{
    public class Service
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal VATRate { get; set; }
        public virtual ICollection<InoviceService> InoviceService { get; set; }
        
    }
}
