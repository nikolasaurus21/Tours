using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelWarrantsReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        private static bool _excursion;
        public TravelWarrantsReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }


        [HttpPost]
        public bool Excursion (bool? excursionOnOff)
        {
            var result = _reportsService.Excursion(excursionOnOff);
            return result;
        }
        
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForClients(int id)
        {
            var result = await _reportsService.GetForClients(id);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var warrants = await _context.Tours.Include(c => c.Client).Where(x => x.ClientId == id).Select(x => new TravelWarrantsReportsDTO
            //{
               
            //    Id = x.Id, 
            //    Mileage=x.Mileage,
            //    Departure=x.Departure,
            //    Destination=x.Destination,
            //    IntermediateDestinations=x.IntermediateDestinations,
            //    ClientName =  x.Client.Name,
            //    DateAndTime=x.TimeOfTour

            //}).ToListAsync();

            //return Ok(warrants);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDestination( string destination)
        {
            var result = await _reportsService.GetForDestination(destination);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //destination = destination.ToLower();

            //var warrants = await _context.Tours
            //    .Where(x => _excursion || x.Client.Excursion != true)
            //    .Where(x =>  x.Destination.ToLower().Equals(destination))
            //    .Select( x => new TravelWarrantsReportsDTO
            //    {
            //        Id= x.Id,
            //        Departure=x.Departure,
            //        Destination=x.Destination,
            //        IntermediateDestinations = x.IntermediateDestinations,
            //        Mileage=x.Mileage,
            //        DateAndTime = x.TimeOfTour,
            //        ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault()

            //    }).ToArrayAsync();

            //return Ok(warrants);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDepAndDes(string departure, string destination)
        {
            var result = await _reportsService.GetForDepAndDes(departure, destination);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //departure = departure.ToLower();
            //destination = destination.ToLower();

            //var warrants = await _context.Tours
            //    .Where(x => _excursion || x.Client.Excursion != true)
            //    .Where(x => x.Departure.ToLower().Equals(departure) && x.Destination.ToLower().Equals(destination))
            //    .Select(x => new TravelWarrantsReportsDTO
            //    {
            //        Id = x.Id,
            //        Departure = x.Departure,
            //        Destination = x.Destination,
            //        IntermediateDestinations = x.IntermediateDestinations,
            //        Mileage = x.Mileage,    
            //        ClientName = _context.Clients.Where(c => c.Id == x.ClientId).Select(c => c.Name).FirstOrDefault(),
            //        DateAndTime = x.TimeOfTour

            //    }).ToArrayAsync();

            //return Ok(warrants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForVehicles(int id)
        {
            var result = await _reportsService.GetForVehicles(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var warrants = await _context.Tours.Include(c => c.Client)
            //    .Where(x => _excursion || x.Client.Excursion != true)
            //    .Where(x => x.VehicleId == id)
            //    .Select(x => new TravelWarrantsReportsDTO
            //    {
            //        Id = x.Id,
            //        Departure = x.Departure,
            //        Destination = x.Destination,
            //        IntermediateDestinations = x.IntermediateDestinations,
            //        Mileage = x.Mileage,
            //        ClientName = x.Client.Name,
            //        DateAndTime= x.TimeOfTour

            //    }).ToArrayAsync();

            //return Ok(warrants);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDrivers(int id )
        {
            var result = await _reportsService.GetForDrivers(id);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var warrants = await _context.Tours.Include(c => c.Client)
            //    .Where(x => _excursion || x.Client.Excursion != true)
            //    .Where(x => x.DriverId == id)
            //    .Select(x => new TravelWarrantsReportsDTO
            //    {
            //        Id = x.Id,
            //        Departure = x.Departure,
            //        Destination = x.Destination,
            //        IntermediateDestinations = x.IntermediateDestinations,
            //        Mileage = x.Mileage,    
            //        ClientName = x.Client.Name,
            //        DateAndTime= x.TimeOfTour

            //    }).ToArrayAsync();

            //return Ok(warrants);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TravelWarrantReportsPeriod>>> GetForPeriod(DateTime from, DateTime to)
        //{


        //    var warrants = await _context.Tours.Include(c => c.Client)
        //        .Where(x => _excursion || x.Client.Excursion != true)
        //        .Where(x => x.TimeOfTour >= from && x.TimeOfTour <= to)
        //        .Select(x => new TravelWarrantReportsPeriod
        //        {
        //            Id = x.Id,
        //            Departure = x.Departure,
        //            Destination = x.Destination,
        //            IntermediateDestinations = x.IntermediateDestinations,
        //            Mileage = x.Mileage,
        //            ClientName = x.Client.Name,
        //            DateAndTime = x.TimeOfTour

        //        }).ToListAsync();

        //    return Ok(warrants);
        //}


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantReportsPeriod>>> GetForPeriod(DateTime from, DateTime to)
        {
           
            var result = await _reportsService.GetForPeriod(from, to);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //IQueryable<Tour> warrants = _context.Tours.Include(c => c.Client)
            //    .Where(x => x.TimeOfTour >= from && x.TimeOfTour <= to);

            //IQueryable<Tour> excursionWarrants = warrants.Where(c => c.Client.Excursion != true);

            //if (_excursion != true)
            //{
            //    warrants = warrants.Except(excursionWarrants);
            //}



            //var result = await warrants.Select(x => new TravelWarrantReportsPeriod
            //{
            //    Id = x.Id,
            //    Departure = x.Departure,
            //    Destination = x.Destination,
            //    IntermediateDestinations = x.IntermediateDestinations,
            //    Mileage = x.Mileage,
            //    ClientName = x.Client.Name,
            //    DateAndTime = x.TimeOfTour

            //}).ToListAsync();

            //return Ok(result);
        }

    }
}
