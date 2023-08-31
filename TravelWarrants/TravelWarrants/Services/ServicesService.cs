using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Services
{
    public class ServicesService : IServicesService
    {
        private readonly TravelWarrantsContext _context;

        public ServicesService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<bool>> DeleteService(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false, Message = false };
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<ServiceDTO>> EditService(int id, ServiceDTOSave serviceDTO)
        {
            var serviceDb = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (serviceDb == null)
            {
                return new ResponseDTO<ServiceDTO>() { IsSucced = false};
            }

            serviceDb.VATRate = serviceDTO.VATRate;
            serviceDb.Name = serviceDTO.Name;

            _context.Services.Update(serviceDb);
            await _context.SaveChangesAsync();

            var updatedService = new ServiceDTO
            {
                Name = serviceDb.Name,
                Id = serviceDb.Id,
                VATRate = serviceDb.VATRate,
            };

            return new ResponseDTO<ServiceDTO>() { IsSucced = true,Message=updatedService };
        }

        public async Task<ResponseDTO<ServiceDTO>> GetService(int id)
        {
            var service = await _context.Services.Where(x => x.Id == id).Select(x => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                VATRate = x.VATRate
            }).FirstOrDefaultAsync();

            return new ResponseDTO<ServiceDTO>() { IsSucced = true, Message = service };
        }

        public async Task<ResponseDTO<IEnumerable<ServiceDTO>>> GetServices()
        {
            var services = await _context.Services.Select(x => new ServiceDTO
            {
                Name = x.Name,
                VATRate = x.VATRate,
                Id = x.Id,
            }).ToListAsync();

            return new ResponseDTO<IEnumerable<ServiceDTO>>() { IsSucced = true, Message = services };
        }

        public async Task<ResponseDTO<ServiceDTO>> NewService(ServiceDTOSave serviceDTO)
        {
            var service = new Service
            {
                Name = serviceDTO.Name,
                VATRate = serviceDTO.VATRate,

            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            var newService = new ServiceDTO
            {
                Name = service.Name,
                Id = service.Id,
                VATRate = service.VATRate,
            };

            return new ResponseDTO<ServiceDTO>() { IsSucced = true, Message = newService };
        }
    }
}
