using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Proinovce;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;
using TravelWarrants.Services;

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
        [HttpGet("{id}")]
        public async Task<ActionResult> GeneratePdf(int id)
        {
            var (pdfBytes, proformaInvoiceNumber) = await _proInoviceService.GeneratePdf(id);
            string fileName = $"Faktura_{proformaInvoiceNumber}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult> GetProformaInvoiceForDelete(int invoiceId)
        {
            var result = await _proInoviceService.GetForDeleteProinvoice(invoiceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
        

        [HttpDelete("{invoiceId}")]
        public async Task<ActionResult> DeleteProformaInvoice(int invoiceId)
        {
            var result  = await _proInoviceService.DeleteProInovice(invoiceId);
            return Ok(result);
        }


        [HttpGet("{invoiceId}")]
        public async Task<ActionResult> DownloadRoutePlan(int invoiceId)
        {
            var fileData = await _proInoviceService.GetRoutePlanFile(invoiceId);
            if (fileData == null || fileData.FileBytes == null || string.IsNullOrWhiteSpace(fileData.FileName))
            {
                return NotFound("Nema plana puta ili proinvoice nije pronađen.");
            }

            

            return File(fileData.FileBytes, "application/octet-stream", fileData.FileName);
        }


        [HttpDelete("{invoiceId}")]
        public async Task<ActionResult> DeleteRoutePlan(int invoiceId)
        {
            var result = await _proInoviceService.DeleteRoutePlan(invoiceId);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{invoiceId}")]
        public async Task<ActionResult> GetProformaInvoiceById(int invoiceId)
        {
            var result = await _proInoviceService.GetProformaInvoiceById(invoiceId);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
       
        [HttpPost]
        public async Task<ActionResult> NewProformaInvoice([FromBody]ProinvoiceNewDTO proinvoiceNewDTO)
        {
            var result = await _proInoviceService.NewProinvoice(proinvoiceNewDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPut("{invoiceId}")]
        public async Task<ActionResult> EditProformaInvoice(int invoiceId,[FromBody]ProinvoiceEditDTO proinvoiceEditDTO)
        {
            var result = await _proInoviceService.EditProinvoice(invoiceId, proinvoiceEditDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetProformaInvoice(int? page)
        {
            var result = await _proInoviceService.GetProformaInvoices(page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
    }
}
