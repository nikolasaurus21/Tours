using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchesController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;
        public SearchesController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchDTOGet>>> GetSearches() 
        {
            var searches = await _context.Accounts.Include(c => c.Client).Select(x => new SearchDTOGet
            {
                Id= x.Id,
                ClientId = (int)x.ClientId,
                ClientName = x.Client.Name,
                Amount = (decimal)x.Amount,
                Date = (DateTime)x.Date,
                Note= x.Note,

            }).ToListAsync();
            return Ok(searches);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<SearchDTOGet>> GetSearch(int id)
        {
            var payment = await _context.Accounts.Include(c => c.Client).Where(x => x.Id == id).Select(x => new SearchDTOGet
            {
                Amount = (decimal)x.Amount,
                Note = x.Note,
                ClientId = (int)x.ClientId,
                ClientName = x.Client.Name,
                Date = (DateTime)x.Date,

            }).FirstOrDefaultAsync();

            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult> NewSearch(SearchesDTOSave searchesDTO)
        {

            var search = new Account
            {
                Date = searchesDTO.Date,
                Amount = searchesDTO.Amount,
                ClientId = searchesDTO.ClientId,
                Note = searchesDTO.Note,
                
            };

            _context.Accounts.Add(search);

            var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == searchesDTO.ClientId);

            if (statuses == null)
            {
                statuses = new Status
                {
                    ClientId = searchesDTO.ClientId,
                    AmountOfAccount = searchesDTO.Amount, 
                    
                };
                _context.Statuses.Add(statuses); 
            }
            else
            {
                statuses.AmountOfAccount += searchesDTO.Amount; 
                statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit; 
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditSearch(int id, SearchesDTOSave searchesDTO)
        {
            var searchDb = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (searchDb == null)
            {
                return NotFound();
            }


            searchDb.Amount = searchesDTO.Amount;
            searchDb.ClientId = searchesDTO.ClientId;
            searchDb.Note = searchesDTO.Note;
            searchDb.Date = searchesDTO.Date;

            _context.Accounts.Update(searchDb);
            await _context.SaveChangesAsync();

            return Ok();




        }
    }
}
