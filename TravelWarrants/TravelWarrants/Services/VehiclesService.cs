using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Clients;
using TravelWarrants.DTOs.Fleet;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly TravelWarrantsContext _context;
        public VehiclesService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<bool>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false, Message = false };
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<FleetDTOGet>> EditVehicle(int id, FleetDTOSave fleetDTO)
        {
            var vehicleDb = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (vehicleDb == null)
            {
                return new ResponseDTO<FleetDTOGet>() { IsSucced = false }; 
            }


            vehicleDb.Name = fleetDTO.Name;
            vehicleDb.Registration = fleetDTO.Registration;
            vehicleDb.Note = fleetDTO.Note;
            vehicleDb.NumberOfSeats = fleetDTO.NumberOfSeats;
            vehicleDb.FuelConsumption = fleetDTO.FuelConsumption;
            vehicleDb.Mileage = fleetDTO.Mileage;

            _context.Vehicles.Update(vehicleDb);
            await _context.SaveChangesAsync();

            var updatedVehicle = new FleetDTOGet
            {
                Id = vehicleDb.Id,
                Registration = vehicleDb.Registration,
                Name = vehicleDb.Name,
                Note = vehicleDb.Note,
                NumberOfSeats = vehicleDb.NumberOfSeats,
                FuelConsumption = vehicleDb.FuelConsumption,
                Mileage = vehicleDb.Mileage,
            };

            return new ResponseDTO<FleetDTOGet>() { IsSucced = true, Message = updatedVehicle };
        }

        public async Task<ResponseDTO<IEnumerable<FleetDTOGet>>> GetFleet()
        {
            var fleet = await _context.Vehicles.Select(x => new FleetDTOGet
            {
                Id = x.Id,
                Registration = x.Registration,
                Name = x.Name,
                Note = x.Note,
                NumberOfSeats = x.NumberOfSeats,
                FuelConsumption = x.FuelConsumption,
                Mileage = x.Mileage,


            }).ToListAsync();

            return new ResponseDTO<IEnumerable<FleetDTOGet>>() { IsSucced = true, Message=fleet };
        }

        public async Task<ResponseDTO<FleetDTOGet>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.Where(x => x.Id == id).Select(x => new FleetDTOGet
            {
                Id = x.Id,
                Name = x.Name,
                Registration = x.Registration,
                Note = x.Note,
                NumberOfSeats = x.NumberOfSeats,
                FuelConsumption = x.FuelConsumption,
                Mileage = x.Mileage


            }).FirstOrDefaultAsync();

            return new ResponseDTO<FleetDTOGet>() { IsSucced = true, Message = vehicle };
        }

        public async Task<ResponseDTO<FleetDTOGet>> NewVehicle(FleetDTOSave fleetDTO)
        {
            var companyExists = await _context.Companies.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<FleetDTOGet>() { IsSucced = false, ErrorMessage = "Add a company first" };
            }

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

            var newVehicle = new FleetDTOGet
            {
                Id = vehicle.Id,
                Registration = vehicle.Registration,
                Name = vehicle.Name,
                Note = vehicle.Note,
                NumberOfSeats = vehicle.NumberOfSeats,
                FuelConsumption = vehicle.FuelConsumption,
                Mileage = vehicle.Mileage,
            };

            return new ResponseDTO<FleetDTOGet>() { IsSucced = true, Message = newVehicle };
        }
    }
}
