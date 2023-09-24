namespace TravelWarrants.DTOs.Tours
{
    public class TourDTOGet
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public decimal Mileage { get; set; }
        public DateTime TimeOfTour { get; set; }
        public string IntermediateDestinations { get; set; }


    }
}
