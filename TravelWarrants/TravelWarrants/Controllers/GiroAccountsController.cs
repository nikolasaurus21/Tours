using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiroAccountsController : ControllerBase
    {
        private readonly IAccountService _accService;

        public GiroAccountsController(IAccountService accService)
        {
            _accService = accService;
        }

        [HttpGet]

        public async Task<ActionResult> Get()
        {

             var result = await _accService.Get();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var acc = await _context.CompaniesGiroAccounts.Select(x => new GiroAccountDTOGet
            //{
            //    Id = x.Id,
            //    Bank = x.Bank,
            //    AccountNumber = x.AccountNumber,
                
            //}).ToListAsync();

            //return Ok(acc);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetAcc(int id)
        {

            var result =  await _accService.GetAcc(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var acc = await _context.CompaniesGiroAccounts.Where(x => x.Id == id).Select(x => new GiroAccountDTOGet
            //{
            //    Id = x.Id,
            //    Bank = x.Bank,
            //    AccountNumber = x.AccountNumber
                
                
            //}).FirstOrDefaultAsync();

            //return Ok(acc);
        }

        [HttpPost]

        public async Task<ActionResult> NewGiroAcc(GiroAccountDTOSave giroAccountDTO)
        {
            
            var result = await _accService.NewGiroAcc(giroAccountDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var acc = new CompanyGiroAccount
            //{
                
            //    Bank = giroAccountDTO.Bank,
            //    AccountNumber= giroAccountDTO.AccountNumber,
            //    CompanyId= 17,
            //};

            //_context.CompaniesGiroAccounts.Add(acc);
            //await _context.SaveChangesAsync();

            //var newAcc = new GiroAccountDTOGet 
            //{
            //    Id= acc.Id,
            //    Bank = acc.Bank,
            //    AccountNumber= acc.AccountNumber,
            //};
            //return Ok(newAcc);
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteGiroAcc(int id)
        {
            var result = await _accService.DeleteGiroAcc(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var accDelete = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);
            //if (accDelete == null)
            //{
            //    return NotFound();
            //}

            //_context.CompaniesGiroAccounts.Remove(accDelete);
            //await _context.SaveChangesAsync();

            //return Ok();
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditGiroAcc(int id, GiroAccountDTOSave giroAccountDTO)
        {
            var result = await _accService.EditGiroAcc(id, giroAccountDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var accDb = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);

            //if(accDb == null)
            //{
            //    return NotFound();
            //}

            //accDb.Bank = giroAccountDTO.Bank;
            //accDb.AccountNumber = giroAccountDTO.AccountNumber;
            

            //_context.CompaniesGiroAccounts.Update(accDb);
            //await _context.SaveChangesAsync();

            //var updateAcc = new GiroAccountDTOGet
            //{
            //    Id = accDb.Id,
            //    Bank = accDb.Bank,
            //    AccountNumber = accDb.AccountNumber,
            //};
            //return Ok(updateAcc);
        }
    }
}
