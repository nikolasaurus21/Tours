using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public CompanyController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CompanyDTO>>> Get()
        {
            var company = await _context.Companies.Select(x => new CompanyDTO
            {
                Id= x.Id,
                Name = x.Name,
                Description= x.Description,
                Address= x.Address,
                PTT= x.PTT,
                Place= x.Place,
                Telephone= x.Telephone,
                Fax= x.Fax,
                MobilePhone= x.MobilePhone,
                TIN= x.TIN,
                VAT= x.VAT
            }).ToListAsync();

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> NewCompany(CompanyDTOSave companyDTO)
        {
            var companyDb = await _context.Companies.FirstOrDefaultAsync();

            if (companyDb == null)
            {

                var newcompany = new Company
                {

                    Name = companyDTO.Name,
                    Description = companyDTO.Description,
                    Address = companyDTO.Address,
                    PTT = companyDTO.PTT,
                    Place = companyDTO.Place,
                    Telephone = companyDTO.Telephone,
                    Fax = companyDTO.Fax,
                    MobilePhone = companyDTO.MobilePhone,
                    TIN = companyDTO.TIN,
                    VAT = companyDTO.VAT
                };
                _context.Companies.Add(newcompany);
            }
            else
            {
                companyDb.Name = companyDTO.Name;
                companyDb.Description = companyDTO.Description;
                companyDb.Address = companyDTO.Address;
                companyDb.PTT = companyDTO.PTT;
                companyDb.Place = companyDTO.Place;
                companyDb.Telephone = companyDTO.Telephone;
                companyDb.Fax = companyDTO.Fax;
                companyDb.MobilePhone = companyDTO.MobilePhone;
                companyDb.TIN = companyDTO.TIN;
                companyDb.VAT = companyDTO.VAT;

            }

            

            await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCompany(int id)
        {
            var deleteC = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteC == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(deleteC);
            await _context.SaveChangesAsync();
            return Ok();
        }

       /* [HttpPut("{id}")]

        public async Task<ActionResult> EditCompany(int id, CompanyDTOSave companyDTO)
        {
           

            var companyDb = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            
            if(companyDb == null)
            {
                return NotFound();
            }
            
            companyDb.Name = companyDTO.Name;
            companyDb.Description = companyDTO.Description;
            companyDb.Address = companyDTO.Address;
            companyDb.PTT = companyDTO.PTT;
            companyDb.Place = companyDTO.Place;
            companyDb.Telephone = companyDTO.Telephone;
            companyDb.Fax = companyDTO.Fax;
            companyDb.MobilePhone = companyDTO.MobilePhone;
            companyDb.TIN = companyDTO.TIN;
            companyDb.VAT = companyDTO.VAT;

            _context.Companies.Update(companyDb);
            await _context.SaveChangesAsync();
            return Ok();    
        }*/
    }
}
