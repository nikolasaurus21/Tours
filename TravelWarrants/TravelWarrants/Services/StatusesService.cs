using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly TravelWarrantsContext _context;
        public StatusesService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<IEnumerable<StatusDTO>>> GetStatuses()
        {
            var statuses = await _context.Statuses.Include(c => c.Client).Select(x => new StatusDTO
            {
                Id = x.Id,
                Client = x.Client.Name,
                Search = x.AmountOfAccount,
                Deposit = x.AmountOfDeposit,
                Balance = x.Balance,
                ClientId = x.ClientId,

            }).ToListAsync();

            return new ResponseDTO<IEnumerable<StatusDTO>> { Message=statuses ,IsSucced=true};
        }
    }
}
