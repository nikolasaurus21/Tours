
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class InovicesService :IInovicesService
    {
        private readonly TravelWarrantsContext _context;
        public InovicesService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<IEnumerable<InoviceGetDTO>>> GetInovices(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var inovices = await _context.Inovices
                .Include(c => c.Client)
                .OrderBy(x => x.DocumentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InoviceGetDTO
                {
                     Id = x.Id,
                     Number = x.Number,
                    Year = x.Year,
                    Amount = x.Total,
                    ClientName = x.Client.Name,
                    Date = x.DocumentDate
                })
                .ToListAsync();

            int totalRecords = await _context.Inovices.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var response = new ResponseDTO<IEnumerable<InoviceGetDTO>>() { Message = inovices, IsSucced = true, TotalPages = totalPages };
            return response;
        }
    }
}
