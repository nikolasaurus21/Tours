using Microsoft.EntityFrameworkCore;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Searches;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class SearchesService : ISearchesService
    {
        private readonly TravelWarrantsContext _context;
        public SearchesService(TravelWarrantsContext context)
        {
            _context = context;   
        }
        public async Task<ResponseDTO<SearchDTOGet>> EditSearch(int id, SearchesDTOSave searchesDTO)
        {
            var searchDb = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (searchDb == null)
            {
                return new ResponseDTO<SearchDTOGet>() { IsSucced = false };
                
                
            }

            var oldClient = searchDb.ClientId;
            var previousAmount = searchDb.Amount;

            searchDb.Amount = searchesDTO.Amount;
            searchDb.ClientId = searchesDTO.ClientId;
            searchDb.Note = searchesDTO.Note;
            searchDb.Date = searchesDTO.Date;

            _context.Accounts.Update(searchDb);

            var oldStatus = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == oldClient);

            if (oldStatus != null)
            {
                oldStatus.AmountOfAccount = (decimal)(oldStatus.AmountOfAccount - previousAmount);
                oldStatus.Balance = oldStatus.AmountOfAccount - oldStatus.AmountOfDeposit;

                if (oldStatus.AmountOfDeposit == 0)
                {
                    _context.Statuses.Remove(oldStatus);
                }
            }


            var statuses = await _context.Statuses.FirstOrDefaultAsync(x => x.ClientId == searchesDTO.ClientId);
            if (statuses == null)
            {
                statuses = new Status
                {
                    ClientId = searchesDTO.ClientId,
                    AmountOfAccount = searchesDTO.Amount,
                };
            }
            else
            {
                statuses.AmountOfAccount += searchesDTO.Amount;
            }

            statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            if (statuses.Id == 0)
            {
                _context.Statuses.Add(statuses);
            }

            await _context.SaveChangesAsync();

            var updatedSearch = new SearchDTOGet
            {
                Id = searchDb.Id,
                Date = (DateTime)searchDb.Date,
                Amount = (decimal)searchDb.Amount,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == searchesDTO.ClientId))?.Name,
                ClientId = (int)searchDb.ClientId,
                Note = searchDb.Note,

            };

            return new ResponseDTO<SearchDTOGet> { Message=updatedSearch,IsSucced=true };
        }

        public async Task<ResponseDTO<SearchDTOGet>> GetSearch(int id)
        {
            var payment = await _context.Accounts.Include(c => c.Client).Where(x => x.Id == id).Select(x => new SearchDTOGet
            {
                Amount = (decimal)x.Amount,
                Note = x.Note,
                ClientId = (int)x.ClientId,
                ClientName = x.Client.Name,
                Date = (DateTime)x.Date,

            }).FirstOrDefaultAsync();

            return new ResponseDTO<SearchDTOGet> { IsSucced=true ,Message=payment};
        }

        public async Task<ResponseDTO<IEnumerable<SearchDTOGet>>> GetSearches()
        {
            var searches = await _context.Accounts.Include(c => c.Client).Select(x => new SearchDTOGet
            {
                Id = x.Id,
                ClientId = (int)x.ClientId,
                ClientName = x.Client.Name,
                Amount = (decimal)x.Amount,
                Date = (DateTime)x.Date,
                Note = x.Note,

            }).ToListAsync();
            return new ResponseDTO<IEnumerable<SearchDTOGet>> { IsSucced=true , Message=searches};
        }

        public async Task<ResponseDTO<SearchDTOGet>> NewSearch(SearchesDTOSave searchesDTO)
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

                };

            }
            statuses.AmountOfAccount += searchesDTO.Amount;
            statuses.Balance = statuses.AmountOfAccount - statuses.AmountOfDeposit;

            if (statuses.Id == 0)
            {

                _context.Statuses.Add(statuses);
            }



            await _context.SaveChangesAsync();

            var newSearch = new SearchDTOGet
            {
                Id = search.Id,
                Date = (DateTime)search.Date,
                Amount = (decimal)search.Amount,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == searchesDTO.ClientId))?.Name,
                Note = search.Note,

            };

            return new ResponseDTO<SearchDTOGet> { Message = newSearch, IsSucced = true };
        }
    }
}
