using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public PaymentsController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentsDTO>>> GetPayments()
        {
            var payments = await _context.Payments.Include(c => c.Client).Select(x => new PaymentsDTO
            {
                Id=x.Id,
                Amount=x.Amount,
                Note=x.Note,
                ClientId=x.ClientId,
                ClientName= x.Client.Name,
                Date=x.Date,
            }).ToListAsync();

            return Ok(payments);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<PaymentsDTO>> GetPayment(int id)
        {
            var payment = await _context.Payments.Include(c => c.Client).Where(x => x.Id == id).Select(x => new PaymentsDTO
            {
                Amount = x.Amount,
                Note = x.Note,
                ClientId = x.ClientId,
                ClientName = x.Client.Name,
                Date = x.Date,

            }).FirstOrDefaultAsync();

            return Ok(payment);
        }
        [HttpPost]
        public async Task<ActionResult> NewPayment(PaymentsDTOSave paymentsDTO)
        {
           
            var payment = new Payment 
            {
                Date= paymentsDTO.Date,
                Amount=paymentsDTO.Amount,
                ClientId=paymentsDTO.ClientId,
                Note=paymentsDTO.Note,
            };

            
            _context.Payments.Add(payment);

            var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == paymentsDTO.ClientId);

            if (statuses == null)
            {
                statuses = new Status
                {
                    ClientId = paymentsDTO.ClientId,
                    AmountOfAccount = paymentsDTO.Amount,
                    
                };
                _context.Statuses.Add(statuses); 
            }
            else
            {
                statuses.AmountOfDeposit += paymentsDTO.Amount; 
                statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit; 
            }

            await _context.SaveChangesAsync();
            

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPayment(int id, PaymentsDTOSave paymentsDTO)
        {
            var paymentDb = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if(paymentDb == null)
            {
                return NotFound();
            }

            
            paymentDb.Amount = paymentsDTO.Amount;
            paymentDb.ClientId = paymentsDTO.ClientId;
            paymentDb.Note = paymentsDTO.Note;
            paymentDb.Date= paymentsDTO.Date;

            _context.Payments.Update(paymentDb);
            await _context.SaveChangesAsync();

            return Ok();


            

        }

       
    }
}
