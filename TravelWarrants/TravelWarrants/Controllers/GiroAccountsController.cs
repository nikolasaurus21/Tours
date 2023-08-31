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

            ;
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

            
        }
    }
}
