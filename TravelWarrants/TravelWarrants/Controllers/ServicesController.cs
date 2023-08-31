using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        public ServicesController(IServicesService servicesService )
        {
            _servicesService = servicesService;
        }

        [HttpGet]

        public async Task<ActionResult> GetServices()
        {

            var result = await _servicesService.GetServices();
            
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var services = await _context.Services.Select(x => new ServiceDTO
            //{
            //    Name= x.Name,
            //    VATRate= x.VATRate,
            //    Id= x.Id,

            //}).ToListAsync(); 

            //return Ok(services);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetService(int id)
        {
            var result = await _servicesService.GetService(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var service = await _context.Services.Where(x => x.Id == id).Select(x => new ServiceDTO
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    VATRate = x.VATRate
            //}).FirstOrDefaultAsync();

            //return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> NewService (ServiceDTOSave serviceDTO)
        {
            var result = await _servicesService.NewService(serviceDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            
            return NotFound();

            //var service = new Service
            //{
            //    Name = serviceDTO.Name,
            //    VATRate= serviceDTO.VATRate,
                
            //};

            //_context.Services.Add(service);
            //await _context.SaveChangesAsync();

            //var newService = new ServiceDTO
            //{
            //    Name = service.Name,
            //    Id = service.Id,
            //    VATRate = service.VATRate,
            //};

            //return Ok(newService);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteService(int id)
        {
            var result = await _servicesService.DeleteService(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);

            //if (service == null)
            //{
            //    return NotFound();
            //}

            //_context.Services.Remove(service);
            //await _context.SaveChangesAsync();
            //return Ok();

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditService(int id , ServiceDTOSave serviceDTO)
        {
            var result = await _servicesService.EditService(id, serviceDTO);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var serviceDb = await _context.Services.FirstOrDefaultAsync(x=> x.Id == id);
            //if (serviceDb == null) 
            //{
            //    return NotFound();
            //}

            //serviceDb.VATRate = serviceDTO.VATRate;
            //serviceDb.Name= serviceDTO.Name;

            //_context.Services.Update(serviceDb);
            //await _context.SaveChangesAsync();

            //var updatedService = new ServiceDTO
            //{
            //    Name = serviceDb.Name,
            //    Id = serviceDb.Id,
            //    VATRate = serviceDb.VATRate,
            //};

            //return Ok(updatedService);
            
        }
    }
}
