using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;
using TravelWarrants.Models;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }

        [HttpGet]

        public async Task<ActionResult> GetDrivers()
        {
            var result = await _driversService.GetDrivers();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var drivers = await _context.Drivers.Select(x => new DriverDTO
            //{
            //    Name = x.Name,
            //    Id= x.Id,
               

            //}).ToListAsync();

            //return Ok(drivers);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetDriver(int id)
        {
            var result = await _driversService.GetDriver(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var acc = await _context.Drivers.Where(x => x.Id == id).Select(x => new DriverDTO
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    NumberOfPhone = x.NUmberOfPhone


            //}).FirstOrDefaultAsync();

            //return Ok(acc);
        }

        [HttpPost]

        public  async Task<ActionResult > NewDriver (DriverDTOSave driverDto)
        {
            var result = await _driversService.NewDriver(driverDto);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var driver = new Driver
            //{
                
            //    Name = driverDto.Name,
            //    NUmberOfPhone = driverDto.NumberOfPhone
            //};

            //_context.Drivers.Add(driver);
            //await _context.SaveChangesAsync();

            //var newDriver = new DriverDTO { Id = driver.Id, Name = driver.Name };

            //return Ok(newDriver);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteDriver(int id)
        {

             var result = await _driversService.DeleteDriver(id);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var deleteDriver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            //if (deleteDriver == null)
            //{
            //    return NotFound();

            //}

            //_context.Drivers.Remove(deleteDriver);
            //await _context.SaveChangesAsync();

            //return Ok();
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> EditDriver(int id , DriverDTOSave driverDTO)
        {
            var result =await _driversService.EditDriver(id, driverDTO);
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();
            
            //var driverDb = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            //if (driverDb == null)
            //{
            //    return NotFound();
            //}

            //driverDb.Name = driverDTO.Name;
            //driverDb.NUmberOfPhone = driverDTO.NumberOfPhone;
            

            //_context.Drivers.Update(driverDb);
            //await _context.SaveChangesAsync();

            //var updatedDriver = new DriverDTO { Id = driverDb.Id, Name = driverDb.Name };
            //return Ok(updatedDriver);
        }
    }
}
