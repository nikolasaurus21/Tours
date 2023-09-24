using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;
using TravelWarrants.DTOs.Company;
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
                return Ok(result.Message);
            }
            return NotFound();

            
        }

        [HttpPost]
        public async Task<ActionResult> NewCompany(CompanyDTOSave companyDTO)
        {

             await _companyService.NewCompany(companyDTO);
            return Ok(companyDTO);

           
        }

       
    }
}
