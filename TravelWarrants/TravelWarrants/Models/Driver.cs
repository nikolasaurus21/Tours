namespace TravelWarrants.Models
{
    public class Driver
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string NUmberOfPhone { get; set; }
        public virtual ICollection<Tour> Tour { get; }
    }
}
