using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Models;
using TravelWarrants.Services;
using TravelWarrants.Interfaces;
using TravelWarrants.DTOs.Clients;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {   
        private readonly IClientsService _clientsService;
        public ClientsController(IClientsService clientsService) 
        {
            _clientsService = clientsService;
        }  
        

        [HttpGet]
        public async Task<ActionResult> GetClients()

        {
            var result = await _clientsService.GetClients();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            
            return NotFound();

           
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetClient(int id)
        {

            var result = await _clientsService.GetClient(id);

            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

           
        }

        [HttpPost]
        public async Task<ActionResult> NewClient(ClientDTOSave clientDTO)
        {

            var result = await _clientsService.NewClient(clientDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.ErrorMessage);

            
        }


        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeleteClient(int id)
        {

            var result = await _clientsService.DeleteClient(id);
            if(result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditClient (int id,ClientDTOSave clientDTO)
        {
            var result = await _clientsService.EditClient(id, clientDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return BadRequest();
            
            
        }
    }
}
