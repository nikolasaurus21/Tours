using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Payments;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;
        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPayments()
        {
            var result = await _paymentsService.GetPayments();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetPayment(int id)
        {
            var result = await _paymentsService.GetPayment(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }
        [HttpPost]
        public async Task<ActionResult> NewPayment(PaymentsDTOSave paymentsDTO)
        {
            var result = await _paymentsService.NewPayment(paymentsDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPayment(int id, PaymentsDTOSave paymentsDTO)
        {

            var result = await _paymentsService.EditPayment(id, paymentsDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }




    }
}
