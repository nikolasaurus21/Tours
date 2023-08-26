using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly TravelWarrantsContext _context;

        public DriversController(TravelWarrantsContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<DriverDTO>>> GetDrivers()
        {
            var drivers = await _context.Drivers.Select(x => new DriverDTO
            {
                Name = x.Name,
                Id= x.Id,
                //NumberOfPhone = x.NUmberOfPhone

            }).ToListAsync();

            return Ok(drivers);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<DriverDTO>> GetDriver(int id)
        {
            var acc = await _context.Drivers.Where(x => x.Id == id).Select(x => new DriverDTO
            {
                Id = x.Id,
                Name = x.Name,
                NumberOfPhone = x.NUmberOfPhone


            }).FirstOrDefaultAsync();

            return Ok(acc);
        }

        [HttpPost]

        public  async Task<ActionResult> NewDriver (DriverDTOSave driverDto)
        {
            //na ovaj kontroler necu provjeravati da li postoji vec cu odma kreirati novi driver!!!!!

            var driver = new Driver
            {
                
                Name = driverDto.Name,
                NUmberOfPhone = driverDto.NumberOfPhone
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteDriver(int id)
        {
            var deleteDriver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if(deleteDriver == null)
            {
                return NotFound();

            }

            _context.Drivers.Remove(deleteDriver);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditDriver(int id , DriverDTOSave driverDTO)
        {
            
            var driverDb = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if (driverDb == null)
            {
                return NotFound();
            }

            driverDb.Name = driverDTO.Name;
            driverDb.NUmberOfPhone = driverDTO.NumberOfPhone;
            

            _context.Drivers.Update(driverDb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
