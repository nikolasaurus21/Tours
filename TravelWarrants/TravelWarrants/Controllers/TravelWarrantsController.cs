using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs.Tours;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelWarrantsController : ControllerBase
    {
        private readonly IToursService _toursService;

        public TravelWarrantsController(IToursService toursService)
        {
            _toursService = toursService;

        }



        [HttpGet]


        public async Task<ActionResult> GetTours()
        {

            var result = await _toursService.GetTours();

            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }
        [HttpGet("{id}")]

        public async Task<ActionResult> GetTour(int id)
        {

            var result = await _toursService.GetTour(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetForDelete(int id)
        {
            var result = await _toursService.GetForDelete(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();


        }


        [HttpPost]
        public async Task<ActionResult> CreateTour(TourDTOSave tourDTO)
        {


            var result = await _toursService.CreateTour(tourDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteTour(int id)
        {
            var result = await _toursService.DeleteTour(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditTour(int id, TourDTOSave tourDTO)
        {
            var result = await _toursService.EditTour(id, tourDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();



        }
    }
}