
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.Interfaces;
using System.Text;
using TravelWarrants.Models;

namespace TravelWarrants.Services
{


    public class InovicesService : IInovicesService
    {
        private readonly TravelWarrantsContext _context;
        private readonly ICompanyService _companyService;
        public InovicesService(TravelWarrantsContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }


        public async Task<ResponseDTO<InoviceGetByIdDeleteDTO>> GetForDelete(int inoviceId)
        {
            var inoviceToDelete = await _context.Inovices.Include(c => c.Client)
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
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            int totalRecords = await _context.Inovices.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var response = new ResponseDTO<IEnumerable<InoviceGetDTO>>() { Message = inovices, IsSucced = true, TotalPages = totalPages };
            return response;
        }
        public async Task<ResponseDTO<InoviceGetByIdDTO>> GetById(int inoviceId)
        {
            var inovice = await _context.Inovices
                .Include(i => i.InoviceService)
                .Include(c => c.Client)
                .Where(x => x.Id == inoviceId)
                .FirstOrDefaultAsync();

            if (inovice == null)
            {
                return new ResponseDTO<InoviceGetByIdDTO> { IsSucced = false };
            }

            var response = new InoviceGetByIdDTO
            {
                ClientId = inovice.ClientId,
                ClientName = inovice.Client.Name,
                DocumentDate = inovice.DocumentDate,
                PaymentDeadline = (inovice.CurrencyDate - inovice.DocumentDate).Days,
                PriceWithoutVAT = inovice.PriceWithoutVAT ?? default(bool),
                Note = inovice.Note,
                Number = inovice.Number + "/" + inovice.Year,
                ItemsOnInovice = inovice.InoviceService.Where(i => i.InoviceId.HasValue && !i.ProformaInvoiceId.HasValue)

                .Select(i => new ItemsOnInoviceEdit
                {
                    Id = i.Id,
                    Description = i.Description,
                    ServiceId = i.ServiceId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    NumberOfDays = i.NumberOfDays
                }).ToList()
            };

            return new ResponseDTO<InoviceGetByIdDTO> { IsSucced = true, Message = response };
        }
        public async Task<ResponseDTO<InoviceGetDTO>> NewInovice(InoviceNewDTO inoviceSaveDTO)
        {

            var companyExists = await _context.Companies.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<InoviceGetDTO>() { IsSucced = false, ErrorMessage = "Add a company first" };
            }

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
                    NumberOfDays = item.NumberOfDays,
                    


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

                newInovice.InoviceService.Add(serviceOnInovice);  // Dodavanje usluge na fakturu
                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;
                amount += serviceOnInovice.Value;

            }

            newInovice.NoVAT = inoviceSaveDTO.PriceWithoutVAT ? amount : amount - vat;
            newInovice.VAT = vat;
            newInovice.Total = inoviceSaveDTO.PriceWithoutVAT ? amount + vat : amount;
            try
            {
                newInovice.Number = _context.Inovices.OrderByDescending(n => n.Number)
                    .First(z => z.Year == newInovice.Year).Number + 1;
            }
            catch
            {

                newInovice.Number = 1;
            }

            _context.Inovices.Add(newInovice);

            _context.Accounts.Add(

                new Account
                {
                    Amount = newInovice.Total,
                    Date = newInovice.DocumentDate,
                    Note = newInovice.Note,
                    ClientId = newInovice.ClientId,
                    Inovice = newInovice
                });

            var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == newInovice.ClientId) ??
                new Status { ClientId = newInovice.ClientId };

            status.AmountOfAccount += newInovice.Total;
            status.Balance = status.AmountOfAccount - status.AmountOfDeposit;

            if (status.Id == 0)
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
                Date = newInovice.DocumentDate
            };

            return new ResponseDTO<InoviceGetDTO> { IsSucced = true, Message = addedInovice };
        }
        public async Task<ResponseDTO<InoviceGetDTO>> EditInvoice(int invoiceId, InoviceEditDTO inoviceEditDTO)
        {
            if (inoviceEditDTO == null || inoviceEditDTO.ItemsOnInovice == null)
            {
                return new ResponseDTO<InoviceGetDTO>() { IsSucced = false };
            }

            var existingInvoice = await _context.Inovices.Include(i => i.InoviceService)
                                                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (existingInvoice == null)
            {
                return new ResponseDTO<InoviceGetDTO>() { IsSucced = false, };
            }

            decimal vat = 0;
            var oldClient = existingInvoice.ClientId;
            var oldAmount = existingInvoice.Total;

            
            existingInvoice.ClientId = inoviceEditDTO.ClientId;
            existingInvoice.DocumentDate = inoviceEditDTO.DocumentDate;
            existingInvoice.CurrencyDate = inoviceEditDTO.DocumentDate.AddDays(inoviceEditDTO.PaymentDeadline);
            existingInvoice.PriceWithoutVAT = inoviceEditDTO.PriceWithoutVAT;
            existingInvoice.Note = inoviceEditDTO.Note;
            existingInvoice.Year = inoviceEditDTO.DocumentDate.Year;

            if (inoviceEditDTO.ItemsToDeleteId != null)
            {
                foreach (var Id in inoviceEditDTO.ItemsToDeleteId)
                {
                    var toDelete = await _context.InovicesServices.FirstOrDefaultAsync(i => i.Id == Id);
                    if (toDelete != null)
                    {
                        _context.InovicesServices.Remove(toDelete);
                    }
                }
            }


            foreach (var item in inoviceEditDTO.ItemsOnInovice.Where(x => x.Id != 0))
            {
                var updateItem = existingInvoice.InoviceService.FirstOrDefault(x => x.Id == item.Id);

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

                updateItem.VAT = inoviceEditDTO.PriceWithoutVAT
                    ? item.Value * service.VATRate / 100
                    : item.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                updateItem.VAT = Math.Round(updateItem.VAT, 2);

                vat += updateItem.VAT;

            }


            foreach (var item in inoviceEditDTO.ItemsOnInovice.Where(x => x.Id == 0))
            {
                var serviceOnInovice = new InoviceService()
                {

                    Description = item.Description,
                    ServiceId = item.ServiceId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Value = item.Quantity * item.Price,
                    NumberOfDays = item.NumberOfDays,
                    


                };

                var inovice = item;

                var service = await _context.Services.FirstAsync(x => x.Id == inovice.ServiceId);

                serviceOnInovice.VAT = inoviceEditDTO.PriceWithoutVAT
                    ? serviceOnInovice.Value * service.VATRate / 100
                    : serviceOnInovice.Value * service.VATRate * 100 / (service.VATRate + 100) / 100;

                existingInvoice.InoviceService.Add(serviceOnInovice);  // Dodavanje usluge na fakturu
                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;


            }

            var amount = inoviceEditDTO.ItemsOnInovice.Sum(x => x.Value);

            existingInvoice.NoVAT = inoviceEditDTO.PriceWithoutVAT ? amount : amount - vat;
            existingInvoice.VAT = vat;
            existingInvoice.Total = inoviceEditDTO.PriceWithoutVAT ? amount + vat : amount;


            var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.InoviceId == existingInvoice.Id)
                ?? new Account { InoviceId = existingInvoice.Id, ProformaInvoiceId = null };


            acc.Date = existingInvoice.DocumentDate;
            acc.Amount = existingInvoice.Total;
            acc.ClientId = existingInvoice.ClientId;
            

            var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClient);
            if (oldStatus != null)
            {
                oldStatus.AmountOfAccount -= oldAmount;
                oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == existingInvoice.ClientId);
            if (status == null)
            {
                status = new Status { ClientId = existingInvoice.ClientId, AmountOfAccount = existingInvoice.Total };
            }
            else
            {
                status.AmountOfAccount += existingInvoice.Total;
            }

            status.Balance = status.AmountOfAccount - status.AmountOfDeposit;
            if (status.Id == 0)
            {
                _context.Statuses.Add(status);
            }

            _context.SaveChanges();

            var addedInovice = new InoviceGetDTO
            {
                Id = existingInvoice.Id,
                Number = existingInvoice.Number,
                Year = existingInvoice.Year,
                Amount = existingInvoice.Total,
                ClientName = await _context.Clients
                .Where(x => x.Id == existingInvoice.ClientId)
                .Select(x => x.Name).FirstOrDefaultAsync(),
                Date = existingInvoice.DocumentDate
            };

            return new ResponseDTO<InoviceGetDTO> { IsSucced = true, Message = addedInovice };
        }
        public async Task<ResponseDTO<bool>> DeleteInovice(int inoviceId)
        {
            var deleteInovice = await _context.Inovices.Include(i => i.InoviceService)
                .FirstOrDefaultAsync(x => x.Id == inoviceId);

            if (deleteInovice == null)
            {
                return new ResponseDTO<bool> { IsSucced = false, Message = false };
            }

            try
            {
                var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.InoviceId == deleteInovice.Id);
                if (acc != null)
                {
                    _context.Accounts.Remove(acc);
                }
                foreach (var item in deleteInovice.InoviceService.ToList())
                {
                    _context.InovicesServices.Remove(item);
                }

                _context.Inovices.Remove(deleteInovice);

                var status = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == deleteInovice.ClientId);
                if (status != null)
                {
                    status.AmountOfAccount -= deleteInovice.Total;
                    status.Balance = status.AmountOfAccount - status.AmountOfDeposit;
                }

                await _context.SaveChangesAsync();
                return new ResponseDTO<bool> { IsSucced = true, Message = true };
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

                return new ResponseDTO<bool> { IsSucced = false, Message = false };
            }
        }
        private async Task<ResponseDTO<InviocePdf>> InoviceForPDf(int id)
        {
            var invoice = await _context.Inovices
                .Include(i => i.InoviceService.Where(i => i.InoviceId.HasValue && !i.ProformaInvoiceId.HasValue))
                .ThenInclude(s => s.Service)
                .Include(c => c.Client)

                .Where(x => x.Id == id)
                .Select(x => new InviocePdf
                {
                    Id = x.Id,
                    ClientName = x.Client.Name,
                    ClientAddress = x.Client.Address,
                    ClientPlace = x.Client.PlaceName,
                    Email = x.Client.Email,
                    Number = x.Number + "/" + x.Year,
                    Total = x.Total,
                    PriceWithoutVat = x.NoVAT,
                    Vat = x.VAT,
                    ItemsOnInovice = x.InoviceService
                    
                    .Select(i => new ItemsOnInovicePdf
                    {

                        Description = i.Description,
                        Service = i.Service.Name,
                        Quantity = i.Quantity,
                        Price = i.Price,
                        NumberOfDays = i.NumberOfDays
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (invoice != null)
            {
                return new ResponseDTO<InviocePdf>
                {
                    IsSucced = true,
                    Message = invoice
                };
            }
            else
            {
                return new ResponseDTO<InviocePdf>
                {
                    IsSucced = false

                };
            }
        }
        public async Task<(byte[],string)> GeneratePdf(int id)
        {
            var companyData = await _companyService.Get();
            if (companyData == null || companyData.Message == null)
            {
                throw new InvalidOperationException("Company data is not available");
            }
            var company = companyData.Message.FirstOrDefault();

            var inoviceData = await InoviceForPDf(id) ?? throw new InvalidOperationException("Inovice data is not available");
            var inovice = inoviceData.Message;
            if (inovice.ItemsOnInovice.Count == 0)
            {
                throw new InvalidOperationException("No items on inovice");
            }


            var document = new PdfDocument();

            var tableRows = new StringBuilder();
            int i = 0;

            foreach (var item in inovice.ItemsOnInovice)
            {
                i++;
                tableRows.Append("<tr><td>" + i.ToString() + "</td>" +
                                 "<td>" + item.Service + "</td>" +
                                 "<td>" + item.Description + "</td>" +
                                 "<td>" + item.NumberOfDays.ToString() + "</td>" +
                                 "<td>" + item.Quantity.ToString() + "</td>" +
                                 "<td>" + item.Price.ToString()+ "€" + "</td></tr>");
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Services", "InvoiceTemplate.html");
            string htmlTemplate = File.ReadAllText(path);

            string htmlContent = htmlTemplate
                .Replace("{{InvoiceNumber}}", inovice.Number)
                .Replace("{{InvoiceDate}}", DateTime.Now.ToString("dd/MM/yyyy"))
                .Replace("{{CompanyName}}", company.Name)
                .Replace("{{CompanyAddress}}", company.Address)
                .Replace("{{CompanyCity}}", company.Place)
                .Replace("{{CompanyPostalCode}}", company.PTT)
                .Replace("{{CompanyPhone}}", company.Telephone)
                .Replace("{{CompanyFax}}", company.Fax)
                .Replace("{{CompanyTIN}}", company.TIN)
                .Replace("{{ClientName}}", inovice.ClientName)
                .Replace("{{ClientAddress}}", inovice.ClientAddress)
                .Replace("{{ClientCity}}", inovice.ClientPlace)
                .Replace("{{ClientEmail}}", inovice.Email)
                .Replace("{{TableRows}}", tableRows.ToString())
                
                .Replace("{{PriceWithoutVat}}", inovice.PriceWithoutVat.ToString() + "€")
                .Replace("{{Vat}}", inovice.Vat.ToString() + "€")
                .Replace("{{Total}}", inovice.Total.ToString() + "€");


            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);

            byte[] response;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            return (response, inovice.Number);
        }
    }

    
}
