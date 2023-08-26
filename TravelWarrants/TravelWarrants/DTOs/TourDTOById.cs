﻿namespace TravelWarrants.DTOs
{
    public class TourDTOById
    {
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

        public decimal StartMileage { get; set; }
        public decimal EndMileage { get; set; }
        public string ClientName { get; set; }
        public string DriverName { get; set; }
        public string VehicleRegistration { get; set; }
        public string Note { get; set; }
        public int? DriverId { get; set; }
        public decimal? FuelPrice { get; set; }
        public decimal? NumberOfDays { get; set; }
        public string IntermediateDestinations { get; set; }
    }
}
