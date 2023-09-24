using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Fleet;

namespace TravelWarrants.Interfaces
{
    public interface IVehiclesService
    {
        Task<ResponseDTO<IEnumerable<FleetDTOGet>>> GetFleet();
        Task<ResponseDTO<FleetDTOGet>> GetVehicle(int vehicleId);
        Task<ResponseDTO<FleetDTOGet>> EditVehicle(int vehicleId, FleetDTOSave fleetDTO);
        Task<ResponseDTO<bool>> DeleteVehicle(int vehicleId);
        Task<ResponseDTO<FleetDTOGet>> NewVehicle(FleetDTOSave fleetDTO);
    }
}
