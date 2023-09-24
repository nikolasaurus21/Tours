using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Searches;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchesController : ControllerBase
    {
        private readonly ISearchesService _searchesService;
        public SearchesController(ISearchesService searchesService )
        {
            _searchesService = searchesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSearches() 
        {
            var result = await _searchesService.GetSearches();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            
        }
        [HttpGet("{id}")]

        public async Task<ActionResult> GetSearch(int id)
        {

            var result =await _searchesService.GetSearch(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            
        }

        [HttpPost]
        public async Task<ActionResult> NewSearch(SearchesDTOSave searchesDTO)
        {
            var result = await _searchesService.NewSearch(searchesDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditSearch(int id, SearchesDTOSave searchesDTO)
        {
            var result =await  _searchesService.EditSearch(id, searchesDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            

        }

    }
}
