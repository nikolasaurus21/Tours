using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public StatusesController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatuses()
        {
            var statuses = await _context.Statuses.Include(c => c.Client).Select(x => new StatusDTO
            {
                Id= x.Id,
                Client = x.Client.Name,
                Search = x.AmountOfAccount,
                Deposit = x.AmountOfDeposit,
                Balance = x.Balance,
                ClientId= x.ClientId,
                
            }).ToListAsync();

            return Ok(statuses);
        }
    }
}
