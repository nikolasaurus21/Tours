using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.Interfaces;


namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInovicesService _invoicesService;

        public InvoicesController(IInovicesService invoicesService)
        {
            _invoicesService = invoicesService;
        }


        [HttpGet("{invoiceId}")]
        public async Task<ActionResult> GetById(int inoviceId)
        {
            var result = await _invoicesService.GetById(inoviceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }

            return NotFound();
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult> ToDelete(int inoviceId)
        {
            var result = await _invoicesService.GetForDelete(inoviceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> Get(int? pageNumber)
        {
            var result = await _invoicesService.GetInovices(pageNumber);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpPost]

        public async Task<ActionResult> NewInvoice(InvoiceNewDTO inoviceSaveDTO)
        {
            var result = await _invoicesService.NewInovice(inoviceSaveDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{invoiceId}")]

        public async Task<ActionResult> EditInvoice(int inoviceId, InvoiceEditDTO inoviceEditDTO)
        {
            var result = await _invoicesService.EditInvoice(inoviceId, inoviceEditDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpDelete("{invoiceId}")]

        public async Task<ActionResult> DeleteInvoice(int inoviceId)
        {
            var result = await _invoicesService.DeleteInovice(inoviceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GeneratePdf(int id)
        {
            var (pdfBytes, invoiceNumber) = await _invoicesService.GeneratePdf(id);
            string fileName = $"Faktura_{invoiceNumber}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

    }
}
