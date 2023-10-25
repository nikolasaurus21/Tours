using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService _statuesesService;
        public StatusesController(IStatusesService statuesesService)
        {
            _statuesesService = statuesesService;
        }

        [HttpGet]

        public async Task<ActionResult> GetStatuses()
        {
            var result = await _statuesesService.GetStatuses();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }
    }
}
