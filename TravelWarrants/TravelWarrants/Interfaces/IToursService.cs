using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;

namespace TravelWarrants.Interfaces
{
    public interface IToursService
    {
        Task<ResponseDTO<IEnumerable<TourDTOGet>>> GetTours();
        Task<ResponseDTO<TourDTOById>> GetTour(int id);
        Task<ResponseDTO<TourDeleteDTO>> GetForDelete(int id);
        Task<ResponseDTO<TourDTOGet>> CreateTour(TourDTOSave tourDTO);
        Task<ResponseDTO<bool>> DeleteTour(int id);
        Task<ResponseDTO<TourDTOGet>> EditTour(int id, TourDTOSave tourDTO);
    }
}
