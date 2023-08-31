using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchesController : ControllerBase
    {
        private readonly ISearchesService _searchesService;
        public SearchesController(ISearchesService searchesService )
        {
            _searchesService = searchesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSearches() 
        {
            var result = await _searchesService.GetSearches();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var searches = await _context.Accounts.Include(c => c.Client).Select(x => new SearchDTOGet
            //{
            //    Id= x.Id,
            //    ClientId = (int)x.ClientId,
            //    ClientName = x.Client.Name,
            //    Amount = (decimal)x.Amount,
            //    Date = (DateTime)x.Date,
            //    Note= x.Note,

            //}).ToListAsync();
            //return Ok(searches);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult> GetSearch(int id)
        {

            var result =await _searchesService.GetSearch(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var payment = await _context.Accounts.Include(c => c.Client).Where(x => x.Id == id).Select(x => new SearchDTOGet
            //{
            //    Amount = (decimal)x.Amount,
            //    Note = x.Note,
            //    ClientId = (int)x.ClientId,
            //    ClientName = x.Client.Name,
            //    Date = (DateTime)x.Date,

            //}).FirstOrDefaultAsync();

            //return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult> NewSearch(SearchesDTOSave searchesDTO)
        {
            var result = await _searchesService.NewSearch(searchesDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var search = new Account
            //{
            //    Date = searchesDTO.Date,
            //    Amount = searchesDTO.Amount,
            //    ClientId = searchesDTO.ClientId,
            //    Note = searchesDTO.Note,
                
            //};

            //_context.Accounts.Add(search);

            //var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == searchesDTO.ClientId);

            //if (statuses == null)
            //{
            //    statuses = new Status
            //    {
            //        ClientId = searchesDTO.ClientId,
                    
            //    };

            //}
            //    statuses.AmountOfAccount += searchesDTO.Amount; 
            //    statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            //if (statuses.Id == 0)
            //{

            //    _context.Statuses.Add(statuses); 
            //}
            
            

            //await _context.SaveChangesAsync();

            //var newSearch = new SearchDTOGet
            //{
            //    Id = search.Id,
            //    Date = (DateTime)search.Date,
            //    Amount = (decimal)search.Amount,
            //    ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == searchesDTO.ClientId))?.Name,
            //    Note = search.Note,

            //};

            //return Ok(newSearch);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditSearch(int id, SearchesDTOSave searchesDTO)
        {
            var result =await  _searchesService.EditSearch(id, searchesDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            //var searchDb = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            //if (searchDb == null)
            //{
            //    return NotFound();
            //}

            //var oldClient = searchDb.ClientId;
            //var previousAmount = searchDb.Amount;

            //searchDb.Amount = searchesDTO.Amount;
            //searchDb.ClientId = searchesDTO.ClientId;
            //searchDb.Note = searchesDTO.Note;
            //searchDb.Date = searchesDTO.Date;

            //_context.Accounts.Update(searchDb);

            //var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClient);

            //if (oldStatus != null)
            //{
            //    oldStatus.AmountOfAccount = (decimal)(oldStatus.AmountOfAccount - previousAmount);
            //    oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;

            //    if (oldStatus.AmountOfDeposit == 0)
            //    {
            //        _context.Statuses.Remove(oldStatus);
            //    }
            //}


            //var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == searchesDTO.ClientId);
            //if (statuses == null)
            //{
            //    statuses = new Status
            //    {
            //        ClientId = searchesDTO.ClientId,
            //        AmountOfAccount = searchesDTO.Amount,
            //    };
            //}
            //else
            //{
            //    statuses.AmountOfAccount += searchesDTO.Amount;
            //}

            //statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            //if(statuses.Id == 0)
            //{
            //    _context.Statuses.Add(statuses);
            //}

            //await _context.SaveChangesAsync();

            //var updatedSearch = new SearchDTOGet
            //{
            //    Id=searchDb.Id,
            //    Date = (DateTime)searchDb.Date,
            //    Amount = (decimal)searchDb.Amount,
            //    ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == searchesDTO.ClientId))?.Name,
            //    ClientId= (int)searchDb.ClientId,
            //    Note = searchDb.Note,

            //};

            //return Ok(updatedSearch);

        }

    }
}
