using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
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

        [HttpPost]

        public async Task<ActionResult> NewInovice(InoviceSaveDTO inoviceSaveDTO)
        {
            var result = await _inovicesService.NewInovice(inoviceSaveDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
    }
}
