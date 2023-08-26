namespace TravelWarrants.DTOs
{
    public class TravelWarrantsReportsDTO
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ClientName { get; set; }
       
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string IntermediateDestinations { get; set; }
        public decimal Mileage { get; set; }
    }
}
