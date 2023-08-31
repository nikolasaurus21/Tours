using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FleetController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;

        public FleetController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }


        [HttpGet]
        public async Task<ActionResult> GetFleet()
        {
            var result = await _vehiclesService.GetFleet();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }

            return NotFound();

            //var fleet = await _context.Vehicles.Select(x => new FleetDTOGet
            //{
            //    Id= x.Id,
            //    Registration = x.Registration,
            //    Name = x.Name,
            //    Note = x.Note,
            //    NumberOfSeats = x.NumberOfSeats,
            //    FuelConsumption = x.FuelConsumption,
            //    Mileage= x.Mileage,


            //}).ToListAsync();

            //return Ok(fleet);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetVehicle(int id)
        {

            var result = await _vehiclesService.GetVehicle(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


            //var vehicle = await _context.Vehicles.Where(x => x.Id == id).Select(x => new FleetDTOGet
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Registration = x.Registration,
            //    Note = x.Note,
            //    NumberOfSeats= x.NumberOfSeats,
            //    FuelConsumption= x.FuelConsumption,
            //    Mileage = x.Mileage


            //}).FirstOrDefaultAsync();

            //return Ok(vehicle);
        }

        [HttpPost]

        public async Task<ActionResult> NewVehicle(FleetDTOSave fleetDTO)
        {
         
            var result = await _vehiclesService.NewVehicle(fleetDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);

            }
            return NotFound();

            //var vehicle = new Vehicle
            //{
                
            //    Name = fleetDTO.Name,
            //    Registration = fleetDTO.Registration,
            //    Note = fleetDTO.Note,
            //    NumberOfSeats = fleetDTO.NumberOfSeats,
            //    FuelConsumption = fleetDTO.FuelConsumption,
            //    Mileage = fleetDTO.Mileage,
            //};

            //_context.Vehicles.Add(vehicle);
            //await _context.SaveChangesAsync();

            //var newVehicle = new FleetDTOGet
            //{
            //    Id = vehicle.Id,
            //    Registration = vehicle.Registration,
            //    Name = vehicle.Name,
            //    Note = vehicle.Note,
            //    NumberOfSeats = vehicle.NumberOfSeats,
            //    FuelConsumption = vehicle.FuelConsumption,
            //    Mileage = vehicle.Mileage,
            //};

            //return Ok(newVehicle);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var result = await _vehiclesService.DeleteVehicle(id);

            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x=> x.Id == id);

            //if (vehicle == null)
            //{
            //    return NotFound();
            //}

            //_context.Vehicles.Remove(vehicle);
            //await _context.SaveChangesAsync();

            //return Ok(); 
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditVehicle (int id, FleetDTOSave fleetDTO)
        {
            var result = await _vehiclesService.EditVehicle(id, fleetDTO);
            if (result.IsSucced) 
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var vehicleDb = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            //if (vehicleDb == null)
            //{
            //    return NotFound();
            //}

            
            //vehicleDb.Name = fleetDTO.Name;
            //vehicleDb.Registration = fleetDTO.Registration;
            //vehicleDb.Note = fleetDTO.Note;
            //vehicleDb.NumberOfSeats = fleetDTO.NumberOfSeats;
            //vehicleDb.FuelConsumption = fleetDTO.FuelConsumption;
            //vehicleDb.Mileage = fleetDTO.Mileage;

            //_context.Vehicles.Update(vehicleDb);
            //await _context.SaveChangesAsync();

            //var updatedVehicle = new FleetDTOGet
            //{
            //    Id = vehicleDb.Id,
            //    Registration = vehicleDb.Registration,
            //    Name = vehicleDb.Name,
            //    Note = vehicleDb.Note,
            //    NumberOfSeats = vehicleDb.NumberOfSeats,
            //    FuelConsumption = vehicleDb.FuelConsumption,
            //    Mileage = vehicleDb.Mileage,
            //};

            //return Ok(updatedVehicle);
            
        }
    
    }
}
