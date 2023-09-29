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
        public async Task<ResponseDTO<InoviceGetByIdDeleteDTO>> GetForDeleteProinvoice(int inoviceId)
        {
            var inoviceToDelete = await _context.ProformaInvoices.Include(c => c.Client)
                .Where(x => x.Id == inoviceId)
                .Select(x => new InoviceGetByIdDeleteDTO
                {
                    Number = x.Number + "/" + x.Year,
                    Date = x.DocumentDate.ToShortDateString(),
                    ClientName = x.Client.Name,
                    Amount = x.Total
                }).FirstOrDefaultAsync();

            if (inoviceToDelete == null)
            {
                return new ResponseDTO<InoviceGetByIdDeleteDTO> { IsSucced = false };
            }
            return new ResponseDTO<InoviceGetByIdDeleteDTO> { IsSucced = true, Message = inoviceToDelete };

        }
        public async Task<bool> DeleteProInovice(int inoviceId)
        {
            var deleteProinovice = await _context.ProformaInvoices
                .Include(i => i.InoviceService)
                .FirstOrDefaultAsync(x => x.Id == inoviceId);

            if (deleteProinovice == null)
            {
                return false;
            }

            try
            {
                var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.InoviceId == deleteProinovice.Id);
                if (acc != null)
                {
                    _context.Accounts.Remove(acc);
                }
                foreach (var item in deleteProinovice.InoviceService.ToList())
                {
                    _context.InovicesServices.Remove(item);
                }

                _context.ProformaInvoices.Remove(deleteProinovice);

                var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == deleteProinovice.ClientId);
                if (status != null)
                {
                    status.AmountOfAccount -= deleteProinovice.Total;
                    status.Balance = status.AmountOfAccount - status.AmountOfDeposit;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch //(Exception ex)
            {

                //var error = ex.Message;
                //var innerEx = ex.InnerException; 

                //while (innerEx != null)
                //{
                //    error += "\n" + innerEx.Message;
                //    innerEx = innerEx.InnerException;
                //}

                return false;
            }
        }
        public async Task<FileData> GetRoutePlanFile(int invoiceId)
        {
            var proinvoice = await _context.ProformaInvoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            if (proinvoice == null || string.IsNullOrWhiteSpace(proinvoice.Route))
            {
                return null;
            }

            var path = proinvoice.Route;
            var fileName = Path.GetFileName(path);
            var fileBytes = await File.ReadAllBytesAsync(path);

            return new FileData { FileBytes = fileBytes, FileName = fileName };
        }
        public async Task<bool> DeleteRoutePlan(int invoiceId)
        {
            var proinvoice = await _context.ProformaInvoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            if (proinvoice == null)
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(proinvoice.Route))
            {
                return false;
            }

            var filePath = proinvoice.Route;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                return false;
            }

            
            proinvoice.Route = null;
            await _context.SaveChangesAsync();

            return true ;

        }
        public async Task<ResponseDTO<ProinvoiceGetByIdDTO>> GetProInvoiceById(int invoiceId)
        {
            var proinvoice = await _context.ProformaInvoices
                .Include(i => i.InoviceService)
                .Include(c => c.Client)
                .Where(x => x.Id == invoiceId)
                .FirstOrDefaultAsync();

            if (proinvoice == null)
            {
                return new ResponseDTO<ProinvoiceGetByIdDTO> { IsSucced = false };
            }

            var response = new ProinvoiceGetByIdDTO
            {
                ClientId = proinvoice.ClientId,
                ClientName = proinvoice.Client.Name,
                DocumentDate = proinvoice.DocumentDate,
                PaymentDeadline = (proinvoice.CurrencyDate - proinvoice.DocumentDate).Days,
                PriceWithoutVAT = proinvoice.PriceWithoutVAT ?? default,
                Note = proinvoice.Note,
                Number = proinvoice.Number + "/" + proinvoice.Year,
                OfferAccepted = proinvoice.OfferAccepted ?? default,
                FileName = proinvoice.Route,
                ItemsOnInovice = proinvoice.InoviceService.Select(i => new ItemsOnInoviceEdit
                {
                    Id = i.Id,
                    Description = i.Description,
                    ServiceId = i.ServiceId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    NumberOfDays = i.NumberOfDays
                }).ToList()
            };

            return new ResponseDTO<ProinvoiceGetByIdDTO> { IsSucced = true, Message = response };
        }
        public async Task<ResponseDTO<ProinvoiceGetDTO>> EditProinvoice(int invoiceId, ProinvoiceEditDTO proinvoiceEditDTO)
        {
            if (proinvoiceEditDTO == null || proinvoiceEditDTO.ItemsOnInovice == null)
            {
                return new ResponseDTO<ProinvoiceGetDTO>() { IsSucced = false };
            }

            var existingProformaInvoice = await _context.Inovices.Include(i => i.InoviceService)
                                                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (existingProformaInvoice == null)
            {
                return new ResponseDTO<ProinvoiceGetDTO>() { IsSucced = false, };
            }

            decimal vat = 0;
            var oldClient = existingProformaInvoice.ClientId;
            var oldAmount = existingProformaInvoice.Total;

            existingProformaInvoice.ClientId = proinvoiceEditDTO.ClientId;
            existingProformaInvoice.DocumentDate = proinvoiceEditDTO.DocumentDate;
            existingProformaInvoice.CurrencyDate = proinvoiceEditDTO.DocumentDate.AddDays(proinvoiceEditDTO.PaymentDeadline);
            existingProformaInvoice.PriceWithoutVAT = proinvoiceEditDTO.PriceWithoutVAT;
            existingProformaInvoice.Note = proinvoiceEditDTO.Note;
            existingProformaInvoice.Year = proinvoiceEditDTO.DocumentDate.Year;
            existingProformaInvoice.OfferAccepted = proinvoiceEditDTO.OfferAccepted;

            foreach (var Id in proinvoiceEditDTO.ItemsToDeleteId)
            {
                var toDelete = await _context.InovicesServices.FirstOrDefaultAsync(i => i.Id == Id);
                if (toDelete != null)
                {
                    _context.InovicesServices.Remove(toDelete);
                }
            }

            foreach (var item in proinvoiceEditDTO.ItemsOnInovice.Where(x => x.Id != 0))
            {
                var updateItem = existingProformaInvoice.InoviceService.FirstOrDefault(x => x.Id == item.Id);

                if (updateItem == null)
                {
                    continue;
                }

                updateItem.Price = item.Price;
                updateItem.Quantity = item.Quantity;
                updateItem.Description = item.Description;
                updateItem.Value = item.Value;
                updateItem.ServiceId = item.ServiceId;
                updateItem.NumberOfDays = item.NumberOfDays;

                var service = await _context.Services.FirstAsync(x => x.Id == item.ServiceId);

                updateItem.VAT = proinvoiceEditDTO.PriceWithoutVAT
                    ? item.Value * service.VATRate / 100
                    : item.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                updateItem.VAT = Math.Round(updateItem.VAT, 2);

                vat += updateItem.VAT;

            }


            foreach (var item in proinvoiceEditDTO.ItemsOnInovice.Where(x => x.Id == 0))
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

                var service = await _context.Services.FirstAsync(x => x.Id == inovice.ServiceId);

                serviceOnInovice.VAT = proinvoiceEditDTO.PriceWithoutVAT
                    ? serviceOnInovice.Value * service.VATRate / 100
                    : serviceOnInovice.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                existingProformaInvoice.InoviceService.Add(serviceOnInovice);  // Dodavanje usluge na fakturu
                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;


            }

            var amount = proinvoiceEditDTO.ItemsOnInovice.Sum(x => x.Value);

            existingProformaInvoice.NoVAT = proinvoiceEditDTO.PriceWithoutVAT ? amount : amount - vat;
            existingProformaInvoice.VAT = vat;
            existingProformaInvoice.Total = proinvoiceEditDTO.PriceWithoutVAT ? amount + vat : amount;

            if (proinvoiceEditDTO.RoutePlan != null && proinvoiceEditDTO.RoutePlan.Length > 0)
            {
                var folderName = "RoutePlan";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = $"{DateTime.UtcNow.Year}-" +
                    $"{existingProformaInvoice.Number}-{proinvoiceEditDTO.RoutePlan.FileName}";
                var fullPath = Path.Combine(pathToSave, fileName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                // Čuvanje fajla
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    proinvoiceEditDTO.RoutePlan.CopyTo(stream);
                }

                existingProformaInvoice.Route = fullPath;
            }

            var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.InoviceId == existingProformaInvoice.Id)
                 ?? new Account { InoviceId = existingProformaInvoice.Id };

            acc.Date = existingProformaInvoice.DocumentDate;
            acc.Amount = existingProformaInvoice.Total;
            acc.ClientId = existingProformaInvoice.ClientId;

            var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClient);
            if (oldStatus != null)
            {
                oldStatus.AmountOfAccount -= oldAmount;
                oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == existingProformaInvoice.ClientId);
            if (status == null)
            {
                status = new Status { ClientId = existingProformaInvoice.ClientId, AmountOfAccount = existingProformaInvoice.Total };
            }
            else
            {
                status.AmountOfAccount += existingProformaInvoice.Total;
            }

            status.Balance = status.AmountOfAccount - status.AmountOfDeposit;
            if (status.Id == 0)
            {
                _context.Statuses.Add(status);
            }

            _context.SaveChanges();

            var addedInovice = new ProinvoiceGetDTO
            {
                Id = existingProformaInvoice.Id,
                Number = existingProformaInvoice.Number + "/" + existingProformaInvoice.Year,
                
                Amount = existingProformaInvoice.Total,
                ClientName = await _context.Clients
                .Where(x => x.Id == existingProformaInvoice.ClientId)
                .Select(x => x.Name).FirstOrDefaultAsync(),
                Date = existingProformaInvoice.DocumentDate,
                
                
            };

            return new ResponseDTO<ProinvoiceGetDTO> { IsSucced = true, Message = addedInovice };


        }
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

            var newProformaInovice = new ProformaInvoice
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

            _context.ProformaInvoices.Add(newProformaInovice);

            _context.Accounts.Add(

                new Account
                {
                    Amount = newProformaInovice.Total,
                    Date = newProformaInovice.DocumentDate,
                    Note = newProformaInovice.Note,
                    ClientId = newProformaInovice.ClientId,
                    ProformaInvoice = newProformaInovice
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
                Date = newProformaInovice.DocumentDate,
                OfferAccepted = (bool)newProformaInovice.OfferAccepted
            };

            return new ResponseDTO<ProinvoiceGetDTO> { IsSucced = true, Message = addedInovice };
        }
        public async Task<ResponseDTO<IEnumerable<ProinvoiceGetDTO>>> GetProformaInvoices(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var proinvoice = await _context.ProformaInvoices
                .Include(c => c.Client)
                .OrderByDescending(x => x.DocumentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select( x=> new ProinvoiceGetDTO
                {
                    Id=x.Id,
                    ClientName= x.Client.Name,
                    Amount = x.Total,
                    OfferAccepted = x.OfferAccepted ?? false,
                    Date = x.DocumentDate,
                    Number=x.Number + "/" +x.Year
                }).ToListAsync();

            return new ResponseDTO<IEnumerable<ProinvoiceGetDTO>> { IsSucced = true, Message = proinvoice };
        }
    }
}
