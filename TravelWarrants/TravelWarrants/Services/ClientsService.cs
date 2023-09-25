using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Clients;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Services
{
    public class ClientsService : IClientsService
    {
        private readonly TravelWarrantsContext _context;

        public ClientsService(TravelWarrantsContext context)
        {
            _context = context;
        }

        public  async Task<ResponseDTO<bool>> DeleteClient(int id)
        {
            var clientToDelete = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (clientToDelete == null)
            {
                return new ResponseDTO<bool>() { IsSucced= false, Message = false };
            }

            _context.Clients.Remove(clientToDelete);
            await _context.SaveChangesAsync();

            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<ClientDTOGet>> EditClient(int id, ClientDTOSave clientDTO)
        {
            var clientDb = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (clientDb == null)
            {
                return new ResponseDTO<ClientDTOGet>() { IsSucced = false };
            }


            clientDb.Name = clientDTO.Name;
            clientDb.Address = clientDTO.Address;
            clientDb.PlaceName = clientDTO.PlaceName;
            clientDb.RegistrationNUmber = clientDTO.RegistrationNumber;
            clientDb.VAT = clientDTO.VAT;
            clientDb.Telephone = clientDTO.Telephone;
            clientDb.Fax = clientDTO.Fax;
            clientDb.Email = clientDTO.Email;
            clientDb.Excursion = clientDTO.Excursion;


            _context.Clients.Update(clientDb);
            await _context.SaveChangesAsync();

            var updatedClient = new ClientDTOGet
            {
                Id = clientDb.Id,
                Name = clientDb.Name,
                Place = clientDb.PlaceName,
                Address = clientDb.Address,

            };

             return new ResponseDTO<ClientDTOGet>() { Message = updatedClient, IsSucced = true };
        }

        public async Task<ResponseDTO<ClientDTOSave>> GetClient(int id)
        {
            var client = await _context.Clients.Where(x => x.Id == id).Select(x => new ClientDTOSave
            {
                Name = x.Name,
                PlaceName = x.PlaceName,
                Address = x.Address,
                Email = x.Email,
                Telephone = x.Telephone,
                Fax = x.Fax,
                Excursion = x.Excursion,
                VAT = x.VAT,
                RegistrationNumber = x.RegistrationNUmber,


            }).FirstOrDefaultAsync();

            return new ResponseDTO<ClientDTOSave>() { Message = client, IsSucced = true };
        }

        public async Task<ResponseDTO<IEnumerable<ClientDTOGet>>> GetClients()
        {
            var clientData = await _context.Clients.Select(x => new ClientDTOGet
            {
                Name = x.Name,
                Id = x.Id,
                Place = x.PlaceName,
                Address = x.Address,
            }).ToListAsync();

            return new ResponseDTO<IEnumerable<ClientDTOGet>> { Message = clientData, IsSucced = true };

        }

        public async Task<ResponseDTO<ClientDTOGet>> NewClient(ClientDTOSave clientDTO)
        {

            var companyExists = await _context.Companies.AnyAsync(); 
            if (!companyExists)
            {
                return new ResponseDTO<ClientDTOGet>() {IsSucced = false,  ErrorMessage="Add company before add a client"};
            }

            var client = new Client
            {
                Name = clientDTO.Name,
                PlaceName = clientDTO.PlaceName,
                Address = clientDTO.Address,
                Email = clientDTO.Email,
                Telephone = clientDTO.Telephone,
                Fax = clientDTO.Fax,
                Excursion = clientDTO.Excursion,
                VAT = clientDTO.VAT,
                RegistrationNUmber = clientDTO.RegistrationNumber,
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            var newClient = new ClientDTOGet
            {
                Id = client.Id,
                Name = client.Name,
                Place = client.PlaceName,
                Address = client.Address,

            };

            return new ResponseDTO<ClientDTOGet>() { Message = newClient ,IsSucced = true};
        }
    }
}
