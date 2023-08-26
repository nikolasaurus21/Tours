using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelWarrantsReportsController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public TravelWarrantsReportsController(TravelWarrantsContext context)
        {
            _context= context;
        }
        
        
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForClients(int id)
        {
            var warrants = await _context.Tours.Include(c => c.Client).Where(x => x.ClientId == id).Select(x => new TravelWarrantsReportsDTO
            {
               
                Id = x.Id, 
                Mileage=x.Mileage,
                Departure=x.Departure,
                Destination=x.Destination,
                IntermediateDestinations=x.IntermediateDestinations,
                ClientName =  x.Client.Name,
                DateAndTime=x.TimeOfTour

            }).ToListAsync();

            return Ok(warrants);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDestination( string destination)
        {
            var warrants = await _context.Tours
                .Where(x =>  x.Destination.Equals(destination))
                .Select( x => new TravelWarrantsReportsDTO
                {
                    Id= x.Id,
                    Departure=x.Departure,
                    Destination=x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage=x.Mileage,
                   
                    ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault()

                }).ToArrayAsync();

            return Ok(warrants);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDepAndDes(string departure, string destination)
        {
            var warrants = await _context.Tours
                .Where(x => x.Departure.Equals(departure) && x.Destination.Equals(destination))
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,    
                    ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault()

                }).ToArrayAsync();

            return Ok(warrants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForVehicles(int id)
        {
            var warrants = await _context.Tours.Include(c => c.Client)
                .Where(x => x.VehicleId == id)
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,
                    ClientName = x.Client.Name,
                    DateAndTime= x.TimeOfTour

                }).ToArrayAsync();

            return Ok(warrants);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDrivers(int id )
        {
            var warrants = await _context.Tours.Include(c => c.Client)
                .Where(x => x.DriverId == id)
                .Select(x => new TravelWarrantsReportsDTO
                {
                    Id = x.Id,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    IntermediateDestinations = x.IntermediateDestinations,
                    Mileage = x.Mileage,    
                    ClientName = x.Client.Name,
                    DateAndTime= x.TimeOfTour

                }).ToArrayAsync();

            return Ok(warrants);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForPeriod(DateTime from, DateTime to)
        {


            var warrants = await _context.Tours.Include(c => c.Client)
                .Where(x => x.TimeOfTour >= from && x.TimeOfTour <= to)
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

            return Ok(warrants);
        }

    }
}
