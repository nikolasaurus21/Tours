using TravelWarrants.DTOs;
using TravelWarrants.DTOs.GiroAcc;
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
        public async Task<ResponseDTO<bool>> DeleteBankAcc(int id)
        {
            var accDelete = await _context.CompanyBankAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (accDelete == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false, Message = false };
            }

            _context.CompanyBankAccounts.Remove(accDelete);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<BankAccountDTOGet>> EditBankAcc(int id, BankAccountDTOSave giroAccountDTO)
        {
            var accDb = await _context.CompanyBankAccounts.FirstOrDefaultAsync(x => x.Id == id);

            if (accDb == null)
            {
                return new ResponseDTO<BankAccountDTOGet>() { IsSucced = false };
            }

            accDb.Bank = giroAccountDTO.Bank;
            accDb.AccountNumber = giroAccountDTO.AccountNumber;


            _context.CompanyBankAccounts.Update(accDb);
            await _context.SaveChangesAsync();

            var updateAcc = new BankAccountDTOGet
            {
                Id = accDb.Id,
                Bank = accDb.Bank,
                AccountNumber = accDb.AccountNumber,
            };
            return new ResponseDTO<BankAccountDTOGet>() { IsSucced = true, Message = updateAcc };
        }

        public async Task<ResponseDTO<IEnumerable<BankAccountDTOGet>>> Get()
        {
            var acc = await _context.CompanyBankAccounts.Select(x => new BankAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber,

            }).ToListAsync();

            return new ResponseDTO<IEnumerable<BankAccountDTOGet>>() { IsSucced = true, Message = acc };
        }

        public async Task<ResponseDTO<BankAccountDTOGet>> GetAcc(int id)
        {
            var acc = await _context.CompanyBankAccounts.Where(x => x.Id == id).Select(x => new BankAccountDTOGet
            {
                Id = x.Id,
                Bank = x.Bank,
                AccountNumber = x.AccountNumber


            }).FirstOrDefaultAsync();

            return new ResponseDTO<BankAccountDTOGet>() { IsSucced = true, Message = acc };
        }

        public async Task<ResponseDTO<BankAccountDTOGet>> NewBankAcc(BankAccountDTOSave giroAccountDTO)
        {
            var companyExists = await _context.Company.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<BankAccountDTOGet>() { IsSucced = false, ErrorMessage = "Add a company first" };
            }
            var acc = new CompanyBankAccount
            {

                Bank = giroAccountDTO.Bank,
                AccountNumber = giroAccountDTO.AccountNumber,
                CompanyId = 1,
            };

            _context.CompanyBankAccounts.Add(acc);
            await _context.SaveChangesAsync();

            var newAcc = new BankAccountDTOGet
            {
                Id = acc.Id,
                Bank = acc.Bank,
                AccountNumber = acc.AccountNumber,
            };
            return new ResponseDTO<BankAccountDTOGet>() { IsSucced = true, Message = newAcc };
        }
    }
}
