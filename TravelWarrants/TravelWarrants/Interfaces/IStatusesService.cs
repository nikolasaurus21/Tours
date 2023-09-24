using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Status;

namespace TravelWarrants.Interfaces
{
    public interface IStatusesService
    {
        Task<ResponseDTO<IEnumerable<StatusDTO>>> GetStatuses();
    }
}
