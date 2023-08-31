using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class ReportsService : IReportsService
    {
        private readonly TravelWarrantsContext _context;
        private static bool _excursion;
        public ReportsService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public bool Excursion(bool? excursionOnOff)
        {
            if (excursionOnOff != null)
            {
                _excursion = excursionOnOff.Value;
            }

            return _excursion;
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForClients(int id)
        {
            
            var warrants = await _context.Tours.Include(c => c.Client).Where(x => x.ClientId == id).Select(x => new TravelWarrantsReportsDTO
            {

                Id = x.Id,
                Mileage = x.Mileage,
                Departure = x.Departure,
                Destination = x.Destination,
                IntermediateDestinations = x.IntermediateDestinations,
                ClientName = x.Client.Name,
                DateAndTime = x.TimeOfTour

            }).ToListAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>> { Message=warrants,IsSucced=true };
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDepAndDes(string departure, string destination)
        {
            departure = departure.ToLower();
            destination = destination.ToLower();

            var warrants = await _context.Tours
                .Where(x => _excursion || x.Client.Excursion != true)
                .Where(x => x.Departure.ToLower().Equals(departure) && x.Destination.ToLower().Equals(destination))
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,
                    ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault(),
                    DateAndTime = x.TimeOfTour

                }).ToArrayAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>> { Message = warrants,IsSucced = true };
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDestination(string destination)
        {
            destination = destination.ToLower();

            var warrants = await _context.Tours
                .Where(x => _excursion || x.Client.Excursion != true)
                .Where(x => x.Destination.ToLower().Equals(destination))
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,
                    DateAndTime = x.TimeOfTour,
                    ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault()

                }).ToArrayAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>> { Message = warrants, IsSucced = true };
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDrivers(int id)
        {
            var warrants = await _context.Tours.Include(c => c.Client)
                .Where(x => _excursion || x.Client.Excursion != true)
                .Where(x => x.DriverId == id)
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,
                    ClientName = x.Client.Name,
                    DateAndTime = x.TimeOfTour

                }).ToArrayAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>> { Message = warrants, IsSucced = true };
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantReportsPeriod>>> GetForPeriod(DateTime from, DateTime to)
        {
            IQueryable<Tour> warrants = _context.Tours.Include(c => c.Client)
                .Where(x => x.TimeOfTour >= from && x.TimeOfTour <= to);

            IQueryable<Tour> excursionWarrants = warrants.Where(c => c.Client.Excursion != true);

            if (_excursion != true)
            {
                warrants = warrants.Except(excursionWarrants);
            }



            var result = await warrants.Select(x => new TravelWarrantReportsPeriod
            {
                Id = x.Id,
                Departure = x.Departure,
                Destination = x.Destination,
                IntermediateDestinations = x.IntermediateDestinations,
                Mileage = x.Mileage,
                ClientName = x.Client.Name,
                DateAndTime = x.TimeOfTour

            }).ToListAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantReportsPeriod>> { IsSucced=true,Message = result};
        }

        public async Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForVehicles(int id)
        {
            var warrants = await _context.Tours.Include(c => c.Client)
                .Where(x => _excursion || x.Client.Excursion != true)
                .Where(x => x.VehicleId == id)
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,
                    ClientName = x.Client.Name,
                    DateAndTime = x.TimeOfTour

                }).ToArrayAsync();

            return new ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>> { Message = warrants, IsSucced = true };
        }
    }
}
