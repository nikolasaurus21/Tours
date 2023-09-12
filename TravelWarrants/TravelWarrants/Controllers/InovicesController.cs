using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InovicesController : ControllerBase
    {
        private readonly IInovicesService _inovicesService;

        public InovicesController(IInovicesService inovicesService)
        {
            _inovicesService = inovicesService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int? pageNumber)
        {
            var result =await _inovicesService.GetInovices(pageNumber);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
    }
}
