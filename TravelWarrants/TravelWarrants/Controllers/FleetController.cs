using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelWarrants.DTOs;
namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FleetController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;

        public FleetController(TravelWarrantsContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FleetDTOGet>>> GetFleet()
        {
            var fleet = await _context.Vehicles.Select(x => new FleetDTOGet
            {
                Id= x.Id,
                Registration = x.Registration,
                Name = x.Name,
                Note = x.Note,
                NumberOfSeats = x.NumberOfSeats,
                FuelConsumption = x.FuelConsumption,
                Mileage= x.Mileage,


            }).ToListAsync();

            return Ok(fleet);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<FleetDTOGet>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == id).Select(x => new FleetDTOGet
            {
                Id = x.Id,
                Name = x.Name,
                Registration = x.Registration,
                Note = x.Note,
                NumberOfSeats= x.NumberOfSeats,
                FuelConsumption= x.FuelConsumption,
                Mileage = x.Mileage


            }).FirstOrDefaultAsync();

            return Ok(vehicle);
        }

        [HttpPost]

        public async Task<ActionResult> NewVehicle(FleetDTOSave fleetDTO)
        {
            

            var vehicle = new Vehicle
            {
                
                Name = fleetDTO.Name,
                Registration = fleetDTO.Registration,
                Note = fleetDTO.Note,
                NumberOfSeats = fleetDTO.NumberOfSeats,
                FuelConsumption = fleetDTO.FuelConsumption,
                Mileage = fleetDTO.Mileage,
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x=> x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(); 
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditVehicle (int id, FleetDTOSave fleetDTO)
        {
            

            var vehicleDb = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleDb == null)
            {
                return NotFound();
            }

            
            vehicleDb.Name = fleetDTO.Name;
            vehicleDb.Registration = fleetDTO.Registration;
            vehicleDb.Note = fleetDTO.Note;
            vehicleDb.NumberOfSeats = fleetDTO.NumberOfSeats;
            vehicleDb.FuelConsumption = fleetDTO.FuelConsumption;
            vehicleDb.Mileage = fleetDTO.Mileage;

            _context.Vehicles.Update(vehicleDb);
            await _context.SaveChangesAsync();

            return Ok();
        }
    
    }
}
