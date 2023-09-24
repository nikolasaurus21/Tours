using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Drivers;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }

        [HttpGet]

        public async Task<ActionResult> GetDrivers()
        {
            var result = await _driversService.GetDrivers();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

           
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetDriver(int id)
        {
            var result = await _driversService.GetDriver(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            
        }

        [HttpPost]

        public  async Task<ActionResult > NewDriver (DriverDTOSave driverDto)
        {
            var result = await _driversService.NewDriver(driverDto);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            ;
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteDriver(int id)
        {

             var result = await _driversService.DeleteDriver(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditDriver(int id , DriverDTOSave driverDTO)
        {
            var result =await _driversService.EditDriver(id, driverDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            
            
        }
    }
}
