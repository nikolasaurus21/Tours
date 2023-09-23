
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{


    public class InovicesService :IInovicesService
    {
        private readonly TravelWarrantsContext _context;
        public InovicesService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<IEnumerable<InoviceGetDTO>>> GetInovices(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var inovices = await _context.Inovices
                .Include(c => c.Client)
                .OrderBy(x => x.DocumentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InoviceGetDTO
                {
                     Id = x.Id,
                     Number = x.Number,
                    Year = x.Year,
                    Amount = x.Total,
                    ClientName = x.Client.Name,
                    Date = x.DocumentDate
                })
                .ToListAsync();

            int totalRecords = await _context.Inovices.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var response = new ResponseDTO<IEnumerable<InoviceGetDTO>>() { Message = inovices, IsSucced = true, TotalPages = totalPages };
            return response;
        }


        [HttpPost]

        public async Task<ResponseDTO<InoviceGetDTO>> NewInovice(InoviceSaveDTO inoviceSaveDTO)
        {
            if (inoviceSaveDTO == null || inoviceSaveDTO.ItemsOnInovice == null)
            {
                return new ResponseDTO<InoviceGetDTO>() { IsSucced = false };
            }

            decimal amount = 0, vat = 0;

           

            var newInovice = new Inovice
            {
                ClientId = inoviceSaveDTO.ClientId,
                DocumentDate = inoviceSaveDTO.DocumentDate,
                CurrencyDate = inoviceSaveDTO.DocumentDate.AddDays(inoviceSaveDTO.PaymentDeadline),
                PriceWithoutVAT = inoviceSaveDTO.PriceWithoutVAT,
                Note = inoviceSaveDTO.Note,
                Year = inoviceSaveDTO.DocumentDate.Year,
                InoviceService = new List<InoviceService>()
                

            };



            foreach (var item in inoviceSaveDTO.ItemsOnInovice)
            {
                var serviceOnInovice = new InoviceService()
                {

                    Description = item.Description,
                    ServiceId = item.ServiceId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Value = item.Quantity * item.Price,
                    NumberOfDays = item.NumberOfDays
                    
                    
                };
                
                var inovice = item;

                var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == inovice.ServiceId);


                if (service == null)
                {
                    throw new Exception($"Service with ID {serviceOnInovice.ServiceId} not found.");
                }

                serviceOnInovice.VAT = inoviceSaveDTO.PriceWithoutVAT
                    ? serviceOnInovice.Value * service.VATRate / 100
                    : serviceOnInovice.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;
                amount += serviceOnInovice.Value;

                newInovice.InoviceService.Add(serviceOnInovice);  // Dodavanje usluge na fakturu
            }

            newInovice.NoVAT = inoviceSaveDTO.PriceWithoutVAT ? amount : amount - vat;
            newInovice.VAT = vat;
            newInovice.Total = inoviceSaveDTO.PriceWithoutVAT ? amount + vat : amount;
            try
            {
                newInovice.Number =  _context.Inovices.OrderByDescending(n => n.Number)
                    .First(z => z.Year == newInovice.Year).Number +1;
            }
            catch 
            {

                newInovice.Number=1;
            }
            
            _context.Inovices.Add(newInovice);

            _context.Accounts.Add(
                new Account 
                {
                    Amount= newInovice.Total,
                    Date = newInovice.DocumentDate,
                    Note = newInovice.Note,
                    ClientId = newInovice.ClientId,
                    Inovice = newInovice
                });

            var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == newInovice.ClientId) ??
                new Status { ClientId = newInovice.ClientId };

            status.AmountOfAccount += newInovice.Total;
            status.Balance = status.AmountOfAccount - status.AmountOfDeposit;

            if(status.Id  == 0)
            {
                _context.Statuses.Add(status);
            }
          
            await _context.SaveChangesAsync();

            

            var addedInovice = new InoviceGetDTO
            {
                Id = newInovice.Id,
                Number = newInovice.Number,
                Year = newInovice.Year,
                Amount = newInovice.Total,
                ClientName = await _context.Clients
                .Where(x => x.Id == newInovice.ClientId)
                .Select(x => x.Name).FirstOrDefaultAsync(),
                Date=newInovice.DocumentDate
            };

            return new ResponseDTO<InoviceGetDTO> { IsSucced = true, Message = addedInovice };
        }
        
    }
}
