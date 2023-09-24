using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Services;

namespace TravelWarrants.Interfaces
{
    public interface IServicesService
    {
        Task<ResponseDTO<IEnumerable<ServiceDTO>>> GetServices();
        Task<ResponseDTO<ServiceDTO>> GetService(int id);
        Task<ResponseDTO<ServiceDTO>> NewService(ServiceDTOSave serviceDTO);
        Task<ResponseDTO<bool>> DeleteService(int id);
        Task<ResponseDTO<ServiceDTO>> EditService(int id, ServiceDTOSave serviceDTO);
    }
}
