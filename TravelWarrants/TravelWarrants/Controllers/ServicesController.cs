using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Services;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;
        public ServicesController(IServicesService servicesService)
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


        }

        [HttpPost]
        public async Task<ActionResult> NewService(ServiceDTOSave serviceDTO)
        {
            var result = await _servicesService.NewService(serviceDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.ErrorMessage);


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



        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditService(int id, ServiceDTOSave serviceDTO)
        {
            var result = await _servicesService.EditService(id, serviceDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();



        }
    }
}
