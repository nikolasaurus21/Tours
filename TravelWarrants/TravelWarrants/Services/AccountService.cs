using Microsoft.EntityFrameworkCore;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class AccountService : IAccountService
    {
        private readonly TravelWarrantsContext _context;
        public AccountService(TravelWarrantsContext context)
        {
            _context = context;
    }
        public async Task<ResponseDTO<bool>> DeleteGiroAcc(int id)
        {
            var accDelete = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (accDelete == null)
            {
                return new ResponseDTO<bool>() { IsSucced=false,Message=false};
            }

            _context.CompaniesGiroAccounts.Remove(accDelete);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<GiroAccountDTOGet>> EditGiroAcc(int id, GiroAccountDTOSave giroAccountDTO)
        {
            var accDb = await _context.CompaniesGiroAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (accDb == null)
            {
                return new ResponseDTO<GiroAccountDTOGet>() { IsSucced = false };
            }

            accDb.Bank = giroAccountDTO.Bank;
            accDb.AccountNumber = giroAccountDTO.AccountNumber;


            _context.CompaniesGiroAccounts.Update(accDb);
            await _context.SaveChangesAsync();

            var updateAcc = new GiroAccountDTOGet
            {
                Id = accDb.Id,
                Bank = accDb.Bank,
                AccountNumber = accDb.AccountNumber,
            };
            return new ResponseDTO<GiroAccountDTOGet>() { IsSucced = true,Message=updateAcc }; 
        }

        public async Task<ResponseDTO<IEnumerable<GiroAccountDTOGet>>> Get()
        {
            var acc = await _context.CompaniesGiroAccounts.Select(x => new GiroAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber,

            }).ToListAsync();

            return new ResponseDTO<IEnumerable<GiroAccountDTOGet>>() { IsSucced = true, Message = acc };
        }

        public async Task<ResponseDTO<GiroAccountDTOGet>> GetAcc(int id)
        {
            var acc = await _context.CompaniesGiroAccounts.Where(x => x.Id == id).Select(x => new GiroAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber


            }).FirstOrDefaultAsync();

            return new ResponseDTO<GiroAccountDTOGet>() { IsSucced = true, Message = acc };
        }

        public async Task<ResponseDTO<GiroAccountDTOGet>> NewGiroAcc(GiroAccountDTOSave giroAccountDTO)
        {
            var acc = new CompanyGiroAccount
            {

                Bank = giroAccountDTO.Bank,
                AccountNumber = giroAccountDTO.AccountNumber,
                CompanyId = 17,
            };

            _context.CompaniesGiroAccounts.Add(acc);
            await _context.SaveChangesAsync();

            var newAcc = new GiroAccountDTOGet
            {
                Id = acc.Id,
                Bank = acc.Bank,
                AccountNumber = acc.AccountNumber,
            };
            return new ResponseDTO<GiroAccountDTOGet>() { IsSucced = true, Message = newAcc };
        }
    }
}
