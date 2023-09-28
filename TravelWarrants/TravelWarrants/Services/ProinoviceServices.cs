using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.DTOs.Proinovce;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class ProinoviceServices : IProInoviceService
    {
        private readonly TravelWarrantsContext _context;
        private readonly ICompanyService _companyService;

        public ProinoviceServices(TravelWarrantsContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<ResponseDTO<ProinvoiceGetDTO>> NewProinvoice(ProinvoiceNewDTO proinvoiceNewDTO)
        {
            var companyExists = await _context.Companies.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<ProinvoiceGetDTO> { IsSucced = false, ErrorMessage = "Add a company first" };
            }

            if (proinvoiceNewDTO == null || proinvoiceNewDTO.ItemsOnInovice.Count == 0)
            {
                return new ResponseDTO<ProinvoiceGetDTO>() { IsSucced = false };
            }

            decimal amount = 0, vat = 0;

            var newProformaInovice = new Inovice
            {
                ClientId = proinvoiceNewDTO.ClientId,
                DocumentDate = proinvoiceNewDTO.DocumentDate,
                CurrencyDate = proinvoiceNewDTO.DocumentDate.AddDays(proinvoiceNewDTO.PaymentDeadline),
                PriceWithoutVAT = proinvoiceNewDTO.PriceWithoutVAT,
                Note = proinvoiceNewDTO.Note,
                Year = proinvoiceNewDTO.DocumentDate.Year,
                ProinoviceWithoutVAT = proinvoiceNewDTO.ProinoviceWithoutVAT,
                InoviceService = new List<InoviceService>()
            };

            foreach (var item in proinvoiceNewDTO.ItemsOnInovice)
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

                serviceOnInovice.VAT = proinvoiceNewDTO.PriceWithoutVAT
                    ? serviceOnInovice.Value * service.VATRate / 100
                    : serviceOnInovice.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                newProformaInovice.InoviceService.Add(serviceOnInovice);  // Dodavanje usluge na fakturu
                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;
                amount += serviceOnInovice.Value;

            }

            newProformaInovice.NoVAT = proinvoiceNewDTO.PriceWithoutVAT ? amount : amount - vat;
            newProformaInovice.VAT = vat;
            newProformaInovice.Total = proinvoiceNewDTO.PriceWithoutVAT ? amount + vat : amount;
            try
            {
                newProformaInovice.Number = _context.Inovices.OrderByDescending(n => n.Number)
                    .First(z => z.Year == newProformaInovice.Year).Number + 1;
            }
            catch
            {

                newProformaInovice.Number = 1;
            }
            //da dodam novo polje za ProinoviceNumber

            // da pitam deja treba li da korisim frombody i fromform itd
            if (proinvoiceNewDTO.RoutePlan != null && proinvoiceNewDTO.RoutePlan.Length > 0)
            {
                var folderName = "RoutePlan";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = $"{DateTime.UtcNow.Year}-" +
                    $"{newProformaInovice.Number}-{proinvoiceNewDTO.RoutePlan.FileName}";
                var fullPath = Path.Combine(pathToSave, fileName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                // Čuvanje fajla
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    proinvoiceNewDTO.RoutePlan.CopyTo(stream);
                }

                newProformaInovice.Route = fullPath;
            }

            _context.Inovices.Add(newProformaInovice);

            _context.Accounts.Add(

                new Account
                {
                    Amount = newProformaInovice.Total,
                    Date = newProformaInovice.DocumentDate,
                    Note = newProformaInovice.Note,
                    ClientId = newProformaInovice.ClientId,
                    Inovice = newProformaInovice
                });

            var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == newProformaInovice.ClientId) ??
                new Status { ClientId = newProformaInovice.ClientId };

            status.AmountOfAccount += newProformaInovice.Total;
            status.Balance = status.AmountOfAccount - status.AmountOfDeposit;

            if (status.Id == 0)
            {
                _context.Statuses.Add(status);
            }

            await _context.SaveChangesAsync();



            var addedInovice = new ProinvoiceGetDTO
            {
                Id = newProformaInovice.Id,
                Number = newProformaInovice.Number +"/"+ newProformaInovice.Year,
                Amount = newProformaInovice.Total,
                ClientName = await _context.Clients
                .Where(x => x.Id == newProformaInovice.ClientId)
                .Select(x => x.Name).FirstOrDefaultAsync(),
                Date = newProformaInovice.DocumentDate
            };

            return new ResponseDTO<ProinvoiceGetDTO> { IsSucced = true, Message = addedInovice };
        }
    }
}
