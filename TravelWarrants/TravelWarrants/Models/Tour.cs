using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TravelWarrants.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public decimal Mileage { get; set; }
        public int NumberOfPassengers { get; set; }
        public decimal Price { get; set; }
        public decimal Toll { get; set; }
        public decimal Fuel { get; set; }
        public DateTime TimeOfTour { get; set; }
        public int VehicleId { get; set; }
        public decimal? DepartureLatitude { get; set; }
        public decimal? DepartureLongitude { get; set; }
        public decimal? DestinationLatitude { get; set; }
        public decimal? DestinationLongitude { get; set; }
        public decimal StartMileage { get; set; }
        public decimal EndMileage { get; set; }
        public decimal VehicleMileage { get; set; }
        public string Note { get; set; }
        public int? DriverId { get; set; }
        public decimal? FuelPrice { get; set; }
        public decimal? NumberOfDays { get; set; }
        public string IntermediateDestinations { get; set; }
        
        public virtual Client Client { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
