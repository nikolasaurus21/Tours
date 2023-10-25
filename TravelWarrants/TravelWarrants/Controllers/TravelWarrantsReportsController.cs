using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Reports;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelWarrantsReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        public TravelWarrantsReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }


        [HttpPost]
        public bool Excursion(bool? excursionOnOff)
        {
            var result = _reportsService.Excursion(excursionOnOff);
            return result;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForClients(int id)
        {
            var result = await _reportsService.GetForClients(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDestination(string destination)
        {
            var result = await _reportsService.GetForDestination(destination);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDepAndDes(string departure, string destination)
        {
            var result = await _reportsService.GetForDepAndDes(departure, destination);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForVehicles(int id)
        {
            var result = await _reportsService.GetForVehicles(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TravelWarrantsReportsDTO>>> GetForDrivers(int id)
        {
            var result = await _reportsService.GetForDrivers(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelWarrantReportsPeriod>>> GetForPeriod(DateTime from, DateTime to)
        {

            var result = await _reportsService.GetForPeriod(from, to);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }

        [HttpGet]
        public async Task<ActionResult> InoviceReportsForClient(int clientId, int? page)
        {
            var result = await _reportsService.GetInovicesForClient(clientId, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> InoviceReportsForDescription(string description, int? page)
        {
            var result = await _reportsService.GetInovicesForDescription(description, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> InoviceReportsForPeriod(DateTime from, DateTime to, int? page)
        {
            var result = await _reportsService.GetInovicesForPeriod(from, to, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> ProformaInvoiceReportsForClient(int clientId, int? page)
        {
            var result = await _reportsService.GetProformaInvoicesForClient(clientId, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult> ProformaInvoiceReportsForDescription(string description, int? page)
        {
            var result = await _reportsService.GetProformaInvoicesForDescription(description, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult> ProformaInvoiceReportsForPeriod(DateTime from, DateTime to, int? page)
        {
            var result = await _reportsService.GetProformaInvoicesForPeriod(from, to, page);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
        }
    }
}
