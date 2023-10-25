using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Fleet;
using TravelWarrants.Interfaces;

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



        }

        [HttpPost]

        public async Task<ActionResult> NewVehicle(FleetDTOSave fleetDTO)
        {

            var result = await _vehiclesService.NewVehicle(fleetDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);

            }
            return BadRequest(result.ErrorMessage);


        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var result = await _vehiclesService.DeleteVehicle(id);

            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditVehicle(int id, FleetDTOSave fleetDTO)
        {
            var result = await _vehiclesService.EditVehicle(id, fleetDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();



        }

    }
}
