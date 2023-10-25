using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Payments;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly TravelWarrantsContext _context;
        public PaymentsService(TravelWarrantsContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<PaymentsDTO>> EditPayment(int id, PaymentsDTOSave paymentsDTO)
        {
            var paymentDb = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (paymentDb == null)
            {
                return new ResponseDTO<PaymentsDTO>() { IsSucced = false };
            }

            int oldClientId = paymentDb.ClientId;
            var previousAmount = paymentDb.Amount;


            paymentDb.Date = paymentsDTO.Date;
            paymentDb.Amount = paymentsDTO.Amount;
            paymentDb.ClientId = paymentsDTO.ClientId;
            paymentDb.Note = paymentsDTO.Note;

            _context.Payments.Update(paymentDb);



            var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClientId);

            if (oldStatus != null)
            {
                oldStatus.AmountOfDeposit -= previousAmount;
                oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;

                if (oldStatus.AmountOfAccount == 0)
                {
                    _context.Statuses.Remove(oldStatus);
                }
            }

            var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == paymentsDTO.ClientId);

            if (statuses == null)
            {
                statuses = new Status
                {
                    ClientId = paymentsDTO.ClientId,
                    AmountOfDeposit = paymentsDTO.Amount,
                };
            }
            else
            {
                statuses.AmountOfDeposit += paymentsDTO.Amount;
            }


            statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            if (statuses.Id == 0)
            {
                _context.Statuses.Add(statuses);
            }


            await _context.SaveChangesAsync();

            var updatedPayment = new PaymentsDTO
            {
                Amount = paymentDb.Amount,
                Id = paymentDb.Id,
                ClientId = paymentDb.ClientId,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == paymentsDTO.ClientId))?.Name,
                Date = paymentDb.Date,
                Note = paymentDb.Note,
            };

            return new ResponseDTO<PaymentsDTO>() { IsSucced = true, Message = updatedPayment };
        }

        public async Task<ResponseDTO<PaymentsDTO>> GetPayment(int id)
        {
            var payment = await _context.Payments.Include(c => c.Client).Where(x => x.Id == id).Select(x => new PaymentsDTO
            {
                Amount = x.Amount,
                Note = x.Note,
                ClientId = x.ClientId,
                ClientName = x.Client.Name,
                Date = x.Date,
            }).FirstOrDefaultAsync();

            return new ResponseDTO<PaymentsDTO>() { IsSucced = true, Message = payment };
        }

        public async Task<ResponseDTO<IEnumerable<PaymentsDTO>>> GetPayments()
        {
            var payments = await _context.Payments.Include(c => c.Client).Select(x => new PaymentsDTO
            {
                Id = x.Id,
                Amount = x.Amount,
                Note = x.Note,
                ClientId = x.ClientId,
                ClientName = x.Client.Name,
                Date = x.Date,
            }).ToListAsync();

            return new ResponseDTO<IEnumerable<PaymentsDTO>>() { IsSucced = true, Message = payments };
        }

        public async Task<ResponseDTO<PaymentsDTO>> NewPayment(PaymentsDTOSave paymentsDTO)
        {
            var companyExists = await _context.Company.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<PaymentsDTO>() { IsSucced = false, ErrorMessage = "Add a company first" };
            }
            var payment = new Payment
            {
                Date = paymentsDTO.Date,
                Amount = paymentsDTO.Amount,
                ClientId = paymentsDTO.ClientId,
                Note = paymentsDTO.Note,
            };


            _context.Payments.Add(payment);

            var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == paymentsDTO.ClientId);

            if (statuses == null)
            {
                statuses = new Status
                {
                    ClientId = paymentsDTO.ClientId,


                };
            }

            statuses.AmountOfDeposit += paymentsDTO.Amount;
            statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;


            if (statuses.Id == 0)
            {

                _context.Statuses.Add(statuses);
            }




            await _context.SaveChangesAsync();

            var newPayment = new PaymentsDTO
            {
                Amount = payment.Amount,
                Id = payment.Id,
                ClientId = payment.ClientId,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == paymentsDTO.ClientId))?.Name,
                Date = payment.Date,
                Note = payment.Note,

            };


            return new ResponseDTO<PaymentsDTO>() { IsSucced = true, Message = newPayment };
        }
    }
}
