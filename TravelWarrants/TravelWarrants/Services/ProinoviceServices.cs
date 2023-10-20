using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.DTOs.Proinovce;
using TravelWarrants.Interfaces;

using Newtonsoft.Json;
using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace TravelWarrants.Services
{
    public class ProinoviceServices : IProInoviceService
    {
        private readonly TravelWarrantsContext _context;
        private readonly ICompanyService _companyService;
        private readonly IFileUploadService _fileUploadService;

        public ProinoviceServices(TravelWarrantsContext context, ICompanyService companyService, IFileUploadService fileUploadService)
        {
            _context = context;
            _companyService = companyService;
            _fileUploadService = fileUploadService;
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
                var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.ProformaInvoiceId == deleteProinovice.Id);
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
            if (proinvoice == null || proinvoice.UploadedFileId == null)
            {
                return new FileData { FileStream=null };
            }

            var result = await _fileUploadService.DownloadRoutePlan(proinvoice.UploadedFileId.Value);

            return new FileData { FileStream = result.FileStream ,FileName = result.FileName };
            
        }
        public async Task<bool> DeleteRoutePlan(int invoiceId)
        {
            var proinvoice = await _context.ProformaInvoices
                .Include(p => p.UploadedFiles)
                .FirstOrDefaultAsync(x => x.Id == invoiceId);

            if (proinvoice == null || proinvoice.UploadedFileId == null)
            {
                return false;
            }

            
            await _fileUploadService.DeleteFile((int)proinvoice.UploadedFileId);

            proinvoice.UploadedFileId = null;

            _context.ProformaInvoices.Update(proinvoice);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<ResponseDTO<ProinvoiceGetByIdDTO>> GetProformaInvoiceById(int invoiceId)
        {
            var proinvoice = await _context.ProformaInvoices
                .Include(i => i.InoviceService.Where(i => !i.InoviceId.HasValue && i.ProformaInvoiceId.HasValue))
                .Include(c => c.Client)
                .Include(u => u.UploadedFiles)
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
                ProinvoiceWithoutVAT = proinvoice.PriceWithoutVAT ?? default,
                OfferAccepted = proinvoice.OfferAccepted ?? default,
                FileName = proinvoice.UploadedFiles?.FileName,
                FileId = proinvoice.UploadedFileId ?? null,
                ItemsOnInovice = proinvoice.InoviceService
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

            return new ResponseDTO<ProinvoiceGetByIdDTO> { IsSucced = true, Message = response };
        }
        public async Task<ResponseDTO<ProinvoiceGetDTO>> EditProinvoice(int invoiceId, ProinvoiceEditDTO proinvoiceEditDTO)
        {
            if (proinvoiceEditDTO == null || proinvoiceEditDTO.ItemsOnInovice == null)
            {
                return new ResponseDTO<ProinvoiceGetDTO>() { IsSucced = false };
            }

            var existingProformaInvoice = await _context.ProformaInvoices.Include(i => i.InoviceService)
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
            existingProformaInvoice.ProinoviceWithoutVAT = proinvoiceEditDTO.ProinoviceWithoutVAT;


           
            
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

                existingProformaInvoice.InoviceService.Add(serviceOnInovice);  
                serviceOnInovice.VAT = Math.Round(serviceOnInovice.VAT, 2);

                vat += serviceOnInovice.VAT;


            }

            var amount = proinvoiceEditDTO.ItemsOnInovice.Sum(x => x.Value);

            existingProformaInvoice.NoVAT = proinvoiceEditDTO.PriceWithoutVAT ? amount : amount - vat;
            existingProformaInvoice.VAT = vat;
            existingProformaInvoice.Total = proinvoiceEditDTO.PriceWithoutVAT ? amount + vat : amount;

            //dio za fajl cu odje

            if (proinvoiceEditDTO.RoutePlan.HasValue)
            {
                
                existingProformaInvoice.UploadedFileId = proinvoiceEditDTO.RoutePlan.Value;
            }
            else
            {
                
                existingProformaInvoice.UploadedFileId = null;
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

           
            await _context.SaveChangesAsync();

            var addedInovice = new ProinvoiceGetDTO
            {
                Id = existingProformaInvoice.Id,
                Number = existingProformaInvoice.Number + "/" + existingProformaInvoice.Year,
                OfferAccepted = existingProformaInvoice.OfferAccepted ?? false,
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
                newProformaInovice.Number = _context.ProformaInvoices.OrderByDescending(n => n.Number)
                    .First(z => z.Year == newProformaInovice.Year).Number + 1;
            }
            catch
            {

                newProformaInovice.Number = 1;
            }


            //dio za fajl odje

            if (proinvoiceNewDTO.RoutePlan > 0)
            {
                var uploadedFile = await _context.UploadedFiles.FirstOrDefaultAsync(x => x.Id == proinvoiceNewDTO.RoutePlan);
                if (uploadedFile != null)
                {
                    newProformaInovice.UploadedFileId = uploadedFile.Id;
                }
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
                OfferAccepted = newProformaInovice.OfferAccepted ?? false

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
        private async Task<ResponseDTO<ProformaInvoicePdf>> ProformaInvoiceForPDF(int id)
        {
            var invoice = await _context.ProformaInvoices
                .Include(i => i.InoviceService.Where(i => !i.InoviceId.HasValue && i.ProformaInvoiceId.HasValue))
                .ThenInclude(s => s.Service)
                .Include(c => c.Client)
                
                .Where(x => x.Id == id)
                .Select(x => new ProformaInvoicePdf
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
                    ShowVat = x.ProinoviceWithoutVAT ?? false,
                    ItemsOnInovice = x.InoviceService
                    .Select(i => new ItemsOnInovicePdf
                    {
                        ServiceVat = i.Service.VATRate,
                        Description = i.Description,
                        Service = i.Service.Name,
                        Quantity = i.Quantity,
                        Price = i.Price,
                        NumberOfDays = i.NumberOfDays
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (invoice != null)
            {
                return new ResponseDTO<ProformaInvoicePdf>
                {
                    IsSucced = true,
                    Message = invoice
                };
            }
            else
            {
                return new ResponseDTO<ProformaInvoicePdf>
                {
                    IsSucced = false

                };
            }
        }
        public async Task<(byte[], string)> GeneratePdf(int id)
        {
            var companyData = await _companyService.Get();
            if (companyData == null || companyData.Message == null)
            {
                throw new InvalidOperationException("Company data is not available");
            }
            var company = companyData.Message.FirstOrDefault();

            var proformaInvoiceData = await ProformaInvoiceForPDF(id);
            if (proformaInvoiceData == null || proformaInvoiceData.Message == null)
            {
                throw new InvalidOperationException("Inovice data is not available");
            }
            var proformaInvoice = proformaInvoiceData.Message;

            if (proformaInvoice.ItemsOnInovice.Count == 0)
            {
                throw new InvalidOperationException("No items on inovice");
            }

            var vatRates = proformaInvoice.ItemsOnInovice
                                    .Select(x => x.ServiceVat)
                                    .Distinct()
                                    .ToList();

            int totalRows = proformaInvoice.ItemsOnInovice.Count;
            int rowsPerPage = 9; // za prvu stranicu
            int remainingRows = totalRows;

            var tableRows = new StringBuilder();
            int i = 0;

            foreach (var item in proformaInvoice.ItemsOnInovice)
            {
                i++;


                tableRows.Append("<tr><td>" + i.ToString() + "</td>" +
                                 "<td>" + item.Service + "</td>" +
                                 "<td>" + item.Description + "</td>" +
                                 "<td>" + item.NumberOfDays.ToString() + "</td>" +
                                 "<td>" + item.ServiceVat.ToString() + "%"+"</td>" +
                                 "<td>" + item.Quantity.ToString() + "</td>" +
                                 "<td>" + item.Price.ToString() + "€" + "</td></tr>");
                if (i == rowsPerPage)
                {
                    tableRows.Append("<tr style=\"page-break-before: always;\"></tr>");

                    if (remainingRows > 15)
                    {
                        rowsPerPage += 15; // Za naredne stranice, dopušta 15 redova
                    }
                    else
                    {
                        rowsPerPage += remainingRows; // Dodaj sve preostale redove ako ih je manje od 15
                    }
                }

                remainingRows--;
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "PdfTemplates", "ProformaInvoiceTemplate.html");
            string htmlTemplate = File.ReadAllText(path);

            var vatFooterRows = new StringBuilder();

            foreach (var vatRate in vatRates)
            {
                decimal totalForThisVatRate = proformaInvoice.ItemsOnInovice
                                             .Where(x => x.ServiceVat == vatRate)
                                             .Sum(x => x.Price * x.Quantity * vatRate / 100m);

                vatFooterRows.Append("<tr " + (proformaInvoice.ShowVat ? "style=\"display:none;\"" : "") + ">" +
                                     "<td class='no-border' colspan='5'></td>" +
                                     "<td class='tfoot-right no-border'>PDV (" + vatRate + "%):</td>" +
                                     "<td class='no-border'>" + totalForThisVatRate + "€" + "</td></tr>");
            }

            string htmlContent = htmlTemplate
                .Replace("{{ProformaInvoiceNumber}}", proformaInvoice.Number)
                .Replace("{{InvoiceDate}}", DateTime.Now.ToString("dd/MM/yyyy"))
                .Replace("{{CompanyName}}", company.Name)
                .Replace("{{CompanyAddress}}", company.Address)
                .Replace("{{CompanyCity}}", company.Place)
                .Replace("{{CompanyPostalCode}}", company.PTT)
                .Replace("{{CompanyPhone}}", company.Telephone)
                .Replace("{{CompanyFax}}", company.Fax)
                .Replace("{{CompanyTIN}}", company.TIN)
                .Replace("{{ClientName}}", proformaInvoice.ClientName)
                .Replace("{{ClientAddress}}", proformaInvoice.ClientAddress)
                .Replace("{{ClientCity}}", proformaInvoice.ClientPlace)
                .Replace("{{ClientEmail}}", proformaInvoice.Email)
                .Replace("{{TableRows}}", tableRows.ToString())
                .Replace("{{ShowVatStyle}}", proformaInvoice.ShowVat ? "style=\"display:none;\"" : "" )
                .Replace("{{PriceWithoutVat}}", proformaInvoice.PriceWithoutVat.ToString() + "€")
                .Replace("{{Vat}}", proformaInvoice.Vat.ToString() + "€")
                .Replace("{{Total}}", proformaInvoice.Total.ToString() + "€")
                .Replace("{{VatRows}}", vatFooterRows.ToString());





            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();

            await page.SetContentAsync(htmlContent);
          
            var options = new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "2.5cm",
                    Right = "0.5cm",
                    Bottom = "2.5cm",
                    Left = "0.5cm"
                }
            };

            var pdfStream = await page.PdfStreamAsync(options);

            await browser.CloseAsync();

            var ms = new MemoryStream();
            await pdfStream.CopyToAsync(ms);
            ms.Position = 0;

            byte[] response = ms.ToArray();
            return (response, proformaInvoice.Number);
        }
    }
}
