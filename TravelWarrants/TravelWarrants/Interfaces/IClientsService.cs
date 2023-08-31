using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IClientsService
    {
        Task<ResponseDTO<IEnumerable<ClientDTOGet>>> GetClients();
        Task<ResponseDTO<ClientDTOSave>> GetClient(int clientId);
        Task<ResponseDTO<ClientDTOGet>> EditClient(int clientId, ClientDTOSave clientDTO);
        Task<ResponseDTO<bool>> DeleteClient(int clientId);
        Task<ResponseDTO<ClientDTOGet>> NewClient(ClientDTOSave clientDTO);

    }
}
