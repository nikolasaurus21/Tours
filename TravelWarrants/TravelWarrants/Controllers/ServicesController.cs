using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public ServicesController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            var services = await _context.Services.Select(x => new ServiceDTO
            {
                Name= x.Name,
                VATRate= x.VATRate,
                Id= x.Id,

            }).ToListAsync(); 

            return Ok(services);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceDTO>> GetService(int id)
        {
            var service = await _context.Services.Where(x => x.Id == id).Select(x => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                VATRate = x.VATRate
            }).SingleOrDefaultAsync();

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> NewService (ServiceDTOSave serviceDTO)
        {
            var service = new Service
            {
                Name = serviceDTO.Name,
                VATRate= serviceDTO.VATRate,
                
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditService(int id , ServiceDTOSave serviceDTO)
        {
            

            var serviceDb = await _context.Services.FirstOrDefaultAsync(x=> x.Id == id);
            if (serviceDb == null) 
            {
                return NotFound();
            }

            serviceDb.VATRate = serviceDTO.VATRate;
            serviceDb.Name= serviceDTO.Name;

            _context.Services.Update(serviceDb);
            await _context.SaveChangesAsync();


            return Ok();
        }
    }
}
