using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IStatusesService
    {
        Task<ResponseDTO<IEnumerable<StatusDTO>>> GetStatuses();
    }
}
