using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelWarrantsController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        private readonly ILogger<TravelWarrantsController> _logger;
        public TravelWarrantsController(TravelWarrantsContext context, ILogger<TravelWarrantsController> logger)
        {
            _context = context;
            _logger = logger;
        }



        [HttpGet]


        public async Task<ActionResult<IEnumerable<TourDTOGet>>> GetTours()
        {
            var tours = await _context.Tours.Include(c => c.Client).ToListAsync();
            

            if (tours == null)
            {
                return NotFound();
            }


            var toursDTO = tours.Select(x => new TourDTOGet
            {
                Id= x.Id,
                ClientId= x.ClientId,
                Departure= x.Departure,
                Destination= x.Destination,
                Mileage= x.Mileage,
               TimeOfTour= x.TimeOfTour,
                IntermediateDestinations= x.IntermediateDestinations,
                ClientName= x.Client.Name

            });

            return Ok(toursDTO);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<TourDTOById>>> GetTour(int id)
        {
            


            var toursDTO = await _context.Tours.Include(c => c.Client).Include(d => d.Driver).Include(v => v.Vehicle).Where(x => x.Id == id).Select(x => new TourDTOById
            {
                Departure = x.Departure,
                Destination = x.Destination,
                IntermediateDestinations = x.IntermediateDestinations,
                Mileage = x.Mileage,
                NumberOfPassengers = x.NumberOfPassengers,
                Price = x.Price,
                Toll = x.Toll,
                Fuel = x.Fuel,
                FuelPrice = x.FuelPrice,
                StartMileage = x.StartMileage,
                EndMileage = x.EndMileage,
                //VehicleMileage = tourDTO.VehicleMileage,
                Note = x.Note,
                NumberOfDays = x.NumberOfDays,
                DriverId = x.DriverId,
                VehicleId = x.VehicleId,
                ClientId = x.ClientId,
                TimeOfTour = x.TimeOfTour,
                ClientName = x.Client.Name,
                DriverName = x.Driver.Name,
                VehicleRegistration = x.Vehicle.Registration,

            }).FirstOrDefaultAsync();

            return Ok(toursDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourDeleteDTO>> GetForDelete(int id)
        {
            var deleteTour = await _context.Tours
                .Include(c => c.Client)
                .Include(v => v.Vehicle)
                .Include(d => d.Driver)
                .Where(x => x.Id == id)
                .Select(x => new TourDeleteDTO
                {
                    Date=x.TimeOfTour,
                    Client = x.Client.Name,
                    Departure= x.Departure,
                    Destination= x.Destination,
                    Registration= x.Vehicle.Registration,
                    Driver= x.Driver.Name,
                    Mileage= x.Mileage,
                }).FirstOrDefaultAsync();

            return Ok(deleteTour);
        }


        [HttpPost]
        public async Task<ActionResult> CreateTour(TourDTOSave tourDTO)
        {
            

            
            var tour = new Tour
            {
                
                Departure = tourDTO.Departure,
                Destination = tourDTO.Destination,
                IntermediateDestinations = tourDTO.IntermediateDestinations,
                Mileage = tourDTO.Mileage,
                NumberOfPassengers = tourDTO.NumberOfPassengers,
                Price = tourDTO.Price,
                Toll = tourDTO.Toll,
                Fuel = tourDTO.Fuel,
                FuelPrice = tourDTO.FuelPrice,
                StartMileage = tourDTO.StartMileage,
                EndMileage = tourDTO.EndMileage,
                //VehicleMileage = tourDTO.VehicleMileage,
                Note = tourDTO.Note,
                NumberOfDays= tourDTO.NumberOfDays,
                DriverId = tourDTO.DriverId,
                VehicleId = tourDTO.VehicleId,
                ClientId = tourDTO.ClientId,
                TimeOfTour = tourDTO.TimeOfTour,
                
            };
            var vehicle = await _context.Vehicles.FindAsync(tourDTO.VehicleId);

            if (vehicle == null)
            {
                
                return NotFound();
            }
           
            vehicle.Mileage += tourDTO.Mileage;

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteTour(int id)
        {
            var tourToDelete = await _context.Tours.FirstOrDefaultAsync(x => x.Id == id);

            if (tourToDelete == null) 
            {
                return NotFound();
            }

            

            _context.Tours.Remove(tourToDelete);

            var vehicle =await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == tourToDelete.VehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Mileage-= tourToDelete.Mileage;

            
            await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditTour(int id,TourDTOSave tourDTO)
        {
          
            

            var tourInDb = await _context.Tours.FirstOrDefaultAsync(x => x.Id == id);

            if(tourInDb == null)
            {
                return NotFound();
            }

            
            tourInDb.Departure = tourDTO.Departure;
            tourInDb.Destination = tourDTO.Destination;
            tourInDb.Mileage = tourDTO.Mileage;
            tourInDb.NumberOfPassengers = tourDTO.NumberOfPassengers;
            tourInDb.Price = tourDTO.Price;
            tourInDb.Toll = tourDTO.Toll;
            tourInDb.Fuel = tourDTO.Fuel;
            tourInDb.TimeOfTour = tourDTO.TimeOfTour;
            tourInDb.StartMileage = tourDTO.StartMileage;
            tourInDb.EndMileage = tourDTO.EndMileage;
            //tourInDb.VehicleMileage = tourDTO.VehicleMileage;
            tourInDb.Note = tourDTO.Note;
            tourInDb.IntermediateDestinations = tourDTO.IntermediateDestinations;
            tourInDb.DriverId = tourDTO.DriverId;
            tourInDb.VehicleId = tourDTO.VehicleId;
            tourInDb.ClientId = tourDTO.ClientId;

            _context.Tours.Update(tourInDb);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}