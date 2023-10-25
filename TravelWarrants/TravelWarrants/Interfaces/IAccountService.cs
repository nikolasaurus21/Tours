using TravelWarrants.DTOs;
using TravelWarrants.DTOs.GiroAcc;

namespace TravelWarrants.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDTO<IEnumerable<BankAccountDTOGet>>> Get();
        Task<ResponseDTO<BankAccountDTOGet>> GetAcc(int id);
        Task<ResponseDTO<BankAccountDTOGet>> NewBankAcc(BankAccountDTOSave giroAccountDTO);
        Task<ResponseDTO<bool>> DeleteBankAcc(int id);
        Task<ResponseDTO<BankAccountDTOGet>> EditBankAcc(int id, BankAccountDTOSave giroAccountDTO);
    }
}
