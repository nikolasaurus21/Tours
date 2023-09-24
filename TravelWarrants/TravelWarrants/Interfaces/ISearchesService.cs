using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Searches;

namespace TravelWarrants.Interfaces
{
    public interface ISearchesService
    {
        Task<ResponseDTO<IEnumerable<SearchDTOGet>>> GetSearches();
        Task<ResponseDTO<SearchDTOGet>> GetSearch(int id);
        Task<ResponseDTO<SearchDTOGet>> NewSearch(SearchesDTOSave searchesDTO);
        Task<ResponseDTO<SearchDTOGet>> EditSearch(int id, SearchesDTOSave searchesDTO);

    }
}
