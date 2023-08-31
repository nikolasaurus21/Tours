using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDTO<IEnumerable<GiroAccountDTOGet>>> Get();
        Task<ResponseDTO<GiroAccountDTOGet>> GetAcc(int id);
        Task<ResponseDTO<GiroAccountDTOGet>> NewGiroAcc(GiroAccountDTOSave giroAccountDTO);
        Task<ResponseDTO<bool>> DeleteGiroAcc(int id);
        Task<ResponseDTO<GiroAccountDTOGet>> EditGiroAcc(int id, GiroAccountDTOSave giroAccountDTO);
    }
}
