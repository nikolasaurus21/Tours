using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.GiroAcc;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly IAccountService _accService;

        public BankAccountsController(IAccountService accService)
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


        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetAcc(int id)
        {

            var result = await _accService.GetAcc(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            ;
        }

        [HttpPost]

        public async Task<ActionResult> NewBankAcc(BankAccountDTOSave giroAccountDTO)
        {

            var result = await _accService.NewBankAcc(giroAccountDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);


        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBankAcc(int id)
        {
            var result = await _accService.DeleteBankAcc(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditBankAcc(int id, BankAccountDTOSave giroAccountDTO)
        {
            var result = await _accService.EditBankAcc(id, giroAccountDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }
    }
}
