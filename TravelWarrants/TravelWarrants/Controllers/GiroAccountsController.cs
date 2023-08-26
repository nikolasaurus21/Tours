using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiroAccountsController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;

        public GiroAccountsController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<GiroAccountDTOGet>>> Get()
        {
            var acc = await _context.CompaniesGiroAccounts.Select(x => new GiroAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber,
                
            }).ToListAsync();

            return Ok(acc);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<GiroAccountDTOGet>> GetAcc(int id)
        {
            var acc = await _context.CompaniesGiroAccounts.Where(x => x.Id == id).Select(x => new GiroAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber
                
                
            }).FirstOrDefaultAsync();

            return Ok(acc);
        }

        [HttpPost]

        public async Task<ActionResult> NewGiroAcc(GiroAccountDTOSave giroAccountDTO)
        {
            

            var acc = new CompanyGiroAccount
            {
                
                Bank = giroAccountDTO.Bank,
                AccountNumber= giroAccountDTO.AccountNumber,
                CompanyId= 14,
            };

            _context.CompaniesGiroAccounts.Add(acc);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteGiroAcc(int id)
        {
            var accDelete = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (accDelete == null)
            {
                return NotFound();
            }

            _context.CompaniesGiroAccounts.Remove(accDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditGiroAcc(int id, GiroAccountDTOSave giroAccountDTO)
        {
            

            var accDb = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if(accDb == null)
            {
                return NotFound();
            }

            accDb.Bank = giroAccountDTO.Bank;
            accDb.AccountNumber = giroAccountDTO.AccountNumber;
            

            _context.CompaniesGiroAccounts.Update(accDb);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
