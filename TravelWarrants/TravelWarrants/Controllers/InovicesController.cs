using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
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

        public async Task<ActionResult> NewInovice(InoviceNewDTO inoviceSaveDTO)
        {
            var result = await _inovicesService.NewInovice(inoviceSaveDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]

        public async Task<ActionResult>EditInovice(int inoviceId, InoviceEditDTO inoviceEditDTO)
        {
            var result = await _inovicesService.EditInvoice(inoviceId, inoviceEditDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpDelete("{inoviceId}")]

        public async Task<ActionResult> DeleteInovice (int inoviceId)
        {
            var result = await _inovicesService.DeleteInovice(inoviceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
    }
}
