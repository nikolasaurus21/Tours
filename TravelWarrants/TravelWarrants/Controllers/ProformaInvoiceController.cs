using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Proinovce;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProformaInvoiceController : ControllerBase
    {
        private readonly IProInoviceService _proInoviceService;

        public ProformaInvoiceController(IProInoviceService proInoviceService)
        {
            _proInoviceService = proInoviceService;
        }

        [HttpPost]
        public async Task<ActionResult> NewProformaInvoice([FromForm] ProinvoiceNewDTO proinvoiceNewDTO)
        {
            var result = await _proInoviceService.NewProinvoice(proinvoiceNewDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
