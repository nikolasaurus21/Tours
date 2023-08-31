using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]

        public async Task<ActionResult> Get()
        {
            var result = await _companyService.Get();
            if (result.IsSucced)
            {
                return Ok(result);
            }
            return NotFound();

            //var company = await _context.Companies.Select(x => new CompanyDTO
            //{
            //    Id= x.Id,
            //    Name = x.Name,
            //    Description= x.Description,
            //    Address= x.Address,
            //    PTT= x.PTT,
            //    Place= x.Place,
            //    Telephone= x.Telephone,
            //    Fax= x.Fax,
            //    MobilePhone= x.MobilePhone,
            //    TIN= x.TIN,
            //    VAT= x.VAT
            //}).ToListAsync();

            //return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> NewCompany(CompanyDTOSave companyDTO)
        {

             await _companyService.NewCompany(companyDTO);
            return Ok(companyDTO);

            //var companyDb = await _context.Companies.FirstOrDefaultAsync();

            //if (companyDb == null)
            //{

            //    var newcompany = new Company
            //    {

            //        Name = companyDTO.Name,
            //        Description = companyDTO.Description,
            //        Address = companyDTO.Address,
            //        PTT = companyDTO.PTT,
            //        Place = companyDTO.Place,
            //        Telephone = companyDTO.Telephone,
            //        Fax = companyDTO.Fax,
            //        MobilePhone = companyDTO.MobilePhone,
            //        TIN = companyDTO.TIN,
            //        VAT = companyDTO.VAT
            //    };
            //    _context.Companies.Add(newcompany);
            //}
            //else
            //{
            //    companyDb.Name = companyDTO.Name;
            //    companyDb.Description = companyDTO.Description;
            //    companyDb.Address = companyDTO.Address;
            //    companyDb.PTT = companyDTO.PTT;
            //    companyDb.Place = companyDTO.Place;
            //    companyDb.Telephone = companyDTO.Telephone;
            //    companyDb.Fax = companyDTO.Fax;
            //    companyDb.MobilePhone = companyDTO.MobilePhone;
            //    companyDb.TIN = companyDTO.TIN;
            //    companyDb.VAT = companyDTO.VAT;

            //}

            

            //await _context.SaveChangesAsync();


            //return Ok();
        }

       
    }
}
