namespace TravelWarrants.Models
{
    public class Vehicle
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Note { get; set; }
        public string NumberOfSeats { get; set; }
        public string FuelConsumption { get; set; }
        public decimal Mileage { get; set; }
        public DateTime? Change { get; set; }
        public virtual ICollection<Tour> Tour { get; set; }
    }
}
