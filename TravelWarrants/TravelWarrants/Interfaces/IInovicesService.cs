using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IInovicesService
    {
        Task<ResponseDTO<IEnumerable<InoviceGetDTO>>> GetInovices(int? page);
    }
}
