using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.Models;
using TravelWarrants.DTOs;
using TravelWarrants.Services;
using TravelWarrants.Interfaces;

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
        //private readonly TravelWarrantsContext _context;

        //public ClientsController(TravelWarrantsContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<ActionResult> GetClients()

        {
            var result = await _clientsService.GetClients();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            
            return NotFound();

            //var clientData = await _context.Clients.Select(x => new ClientDTOGet {
            //    Name = x.Name,
            //    Id = x.Id,
            //    Place = x.PlaceName,
            //    Address = x.Address,
            //}).ToListAsync();

            //return Ok(clientData);
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

            //var client = await _context.Clients.Where(x => x.Id == id).Select(x => new ClientDTOSave
            //{
            //    Name = x.Name,
            //    PlaceName = x.PlaceName,
            //    Address = x.Address,
            //    Email = x.Email,
            //    Telephone = x.Telephone,
            //    Fax = x.Fax,
            //    Excursion = x.Excursion,
            //    VAT = x.VAT,
            //    RegistrationNumber =x.RegistrationNUmber,


            //}).FirstOrDefaultAsync();

            //return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> NewClient(ClientDTOSave clientDTO)
        {

            var result = await _clientsService.NewClient(clientDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var client = new Client
            //{
            //    Name = clientDTO.Name,
            //    PlaceName = clientDTO.PlaceName,
            //    Address = clientDTO.Address,
            //    Email = clientDTO.Email,
            //    Telephone = clientDTO.Telephone,
            //    Fax = clientDTO.Fax,
            //    Excursion = clientDTO.Excursion,
            //    VAT = clientDTO.VAT,
            //    RegistrationNUmber = clientDTO.RegistrationNumber,
            //};
            //_context.Clients.Add(client);
            //await _context.SaveChangesAsync();

            //var newClient = new ClientDTOGet
            //{
            //    Id = client.Id, 
            //    Name = client.Name,
            //    Place = client.PlaceName,
            //    Address = client.Address,
                
            //};

            //return Ok(newClient);
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

            //var clientToDelete = await _context.Clients.FirstOrDefaultAsync(x=> x.Id == id);
            //if (clientToDelete == null)
            //{
            //    return NotFound();
            //}

            //_context.Clients.Remove(clientToDelete);
            //await _context.SaveChangesAsync();

            //return Ok();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditClient (int id,ClientDTOSave clientDTO)
        {
            var result = await _clientsService.EditClient(id, clientDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            
            //var clientDb = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

            //if (clientDb == null)
            //{
            //    return NotFound();
            //}

            
            //clientDb.Name= clientDTO.Name;
            //clientDb.Address = clientDTO.Address;
            //clientDb.PlaceName= clientDTO.PlaceName;
            //clientDb.RegistrationNUmber = clientDTO.RegistrationNumber;
            //clientDb.VAT= clientDTO.VAT;
            //clientDb.Telephone= clientDTO.Telephone;
            //clientDb.Fax= clientDTO.Fax;
            //clientDb.Email= clientDTO.Email;
            //clientDb.Excursion= clientDTO.Excursion;
            
            
            //_context.Clients.Update(clientDb);
            //await _context.SaveChangesAsync();

            //var updatedClient = new ClientDTOGet
            //{
            //    Id = clientDb.Id,
            //    Name = clientDb.Name,
            //    Place = clientDb.PlaceName,
            //    Address = clientDb.Address,
                
            //};

            //return Ok(updatedClient);
        }
    }
}
