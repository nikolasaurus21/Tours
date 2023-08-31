using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

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
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var payments = await _context.Payments.Include(c => c.Client).Select(x => new PaymentsDTO
            //{
            //    Id=x.Id,
            //    Amount=x.Amount,
            //    Note=x.Note,
            //    ClientId=x.ClientId,
            //    ClientName= x.Client.Name,
            //    Date=x.Date,
            //}).ToListAsync();

            //return Ok(payments);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetPayment(int id)
        {
            var result = await _paymentsService.GetPayment(id);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var payment = await _context.Payments.Include(c => c.Client).Where(x => x.Id == id).Select(x => new PaymentsDTO
            //{
            //    Amount = x.Amount,
            //    Note = x.Note,
            //    ClientId = x.ClientId,
            //    ClientName = x.Client.Name,
            //    Date = x.Date,

            //}).FirstOrDefaultAsync();

            //return Ok(payment);
        }
        [HttpPost]
        public async Task<ActionResult> NewPayment(PaymentsDTOSave paymentsDTO)
        {
            var result = await _paymentsService.NewPayment(paymentsDTO);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
           
            //var payment = new Payment 
            //{
            //    Date= paymentsDTO.Date,
            //    Amount=paymentsDTO.Amount,
            //    ClientId=paymentsDTO.ClientId,
            //    Note=paymentsDTO.Note,
            //};

            
            //_context.Payments.Add(payment);

            //var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == paymentsDTO.ClientId);

            //if (statuses == null)
            //{
            //    statuses = new Status
            //    {
            //        ClientId = paymentsDTO.ClientId,
                    
                    
            //    };
            //}
                
            //    statuses.AmountOfDeposit += paymentsDTO.Amount; 
            //    statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

                
            //if(statuses.Id == 0)
            //{

            //    _context.Statuses.Add(statuses); 
            //}
          
            
            

            //await _context.SaveChangesAsync();

            //var newPayment = new PaymentsDTO
            //{
            //    Amount = payment.Amount,
            //    Id = payment.Id,
            //    ClientId = payment.ClientId,
            //    ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == paymentsDTO.ClientId))?.Name,
            //    Date = payment.Date,
            //    Note = payment.Note,

            //};


            //return Ok(newPayment);
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
            //var paymentDb = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);

            //if (paymentDb == null)
            //{
            //    return NotFound();
            //}

            //int oldClientId = paymentDb.ClientId; 
            //var previousAmount = paymentDb.Amount;

          
            //paymentDb.Date = paymentsDTO.Date;
            //paymentDb.Amount = paymentsDTO.Amount;
            //paymentDb.ClientId = paymentsDTO.ClientId;
            //paymentDb.Note = paymentsDTO.Note;

            //_context.Payments.Update(paymentDb);

            
                
            //var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClientId);

            //if (oldStatus != null)
            //{
            //    oldStatus.AmountOfDeposit -= previousAmount;
            //    oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;

            //    if (oldStatus.AmountOfAccount == 0)
            //    {
            //        _context.Statuses.Remove(oldStatus);
            //    }
            //}

            //var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == paymentsDTO.ClientId);

            //if (statuses == null)
            //{
            //    statuses = new Status
            //    {
            //        ClientId = paymentsDTO.ClientId,
            //        AmountOfDeposit = paymentsDTO.Amount,
            //    };
            //}
            //else
            //{
            //    statuses.AmountOfDeposit+= paymentsDTO.Amount;
            //}

                    
            //statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            //if(statuses.Id == 0)
            //{
            //     _context.Statuses.Add(statuses);
            //}
            

            //await _context.SaveChangesAsync();

            //var updatedPayment = new PaymentsDTO
            //{
            //    Amount = paymentDb.Amount,
            //    Id = paymentDb.Id,
            //    ClientId = paymentDb.ClientId,
            //    ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == paymentsDTO.ClientId))?.Name,
            //    Date = paymentDb.Date,
            //    Note = paymentDb.Note,
            //};

            //return Ok(updatedPayment);
        }




    }
}
