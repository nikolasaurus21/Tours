namespace TravelWarrants.DTOs
{
    public class TourDeleteDTO
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string Registration { get; set; }
        public string Driver { get; set; }
        public decimal Mileage { get; set; }

    }
}
