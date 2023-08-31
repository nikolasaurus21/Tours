using Microsoft.EntityFrameworkCore;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class DriversService : IDriversService
    {
        private readonly TravelWarrantsContext _context;
        public DriversService(TravelWarrantsContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<bool>> DeleteDriver(int id)
        {
            var deleteDriver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if (deleteDriver == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false ,Message=false};
            }

            _context.Drivers.Remove(deleteDriver);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<DriverDTO>> EditDriver(int id, DriverDTOSave driverDTO)
        {
            var driverDb = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if (driverDb == null)
            {
                  return new ResponseDTO<DriverDTO>() { IsSucced = false };
            }

            driverDb.Name = driverDTO.Name;
            driverDb.NUmberOfPhone = driverDTO.NumberOfPhone;


            _context.Drivers.Update(driverDb);
            await _context.SaveChangesAsync();

            var updatedDriver = new DriverDTO { Id = driverDb.Id, Name = driverDb.Name };

            return new ResponseDTO<DriverDTO>() { IsSucced = true,Message=updatedDriver };
        }

        public async Task<ResponseDTO<DriverDTO>> GetDriver(int id)
        {
            var driver = await _context.Drivers.Where(x => x.Id == id).Select(x => new DriverDTO
            {
                Id = x.Id,
                Name = x.Name,
                NumberOfPhone = x.NUmberOfPhone


            }).FirstOrDefaultAsync();

            return new ResponseDTO<DriverDTO>() { IsSucced = true, Message = driver };
        }

        public async Task<ResponseDTO<IEnumerable<DriverDTO>>> GetDrivers()
        {
            var drivers = await _context.Drivers.Select(x => new DriverDTO
            {
                Name = x.Name,
                Id = x.Id,


            }).ToListAsync();

            return new ResponseDTO<IEnumerable<DriverDTO>>() { IsSucced = true, Message = drivers };
        }
    
        public async Task<ResponseDTO<DriverDTO>> NewDriver(DriverDTOSave driverDTO)
        {
            var driver = new Driver
            {

                Name = driverDTO.Name,
                NUmberOfPhone = driverDTO.NumberOfPhone
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            var newDriver = new DriverDTO { Id = driver.Id, Name = driver.Name };

            return new ResponseDTO<DriverDTO>() { IsSucced = true, Message = newDriver };
        }
    }
}
