using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Drivers;

namespace TravelWarrants.Interfaces
{
    public interface IDriversService
    {
        Task<ResponseDTO<IEnumerable<DriverDTO>>> GetDrivers();
        Task<ResponseDTO<DriverDTO>> GetDriver(int id);
        Task<ResponseDTO<DriverDTO>> NewDriver(DriverDTOSave driverDTO);
        Task<ResponseDTO<bool>> DeleteDriver(int id);
        Task<ResponseDTO<DriverDTO>> EditDriver(int id,DriverDTOSave driverDTO);
    }
}
