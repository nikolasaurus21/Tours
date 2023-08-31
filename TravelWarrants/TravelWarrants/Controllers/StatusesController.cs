﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService _statuesesService;
        public StatusesController(IStatusesService statuesesService)
        {
            _statuesesService = statuesesService;
        }

        [HttpGet]

        public async Task<ActionResult> GetStatuses()
        {
            var result = await _statuesesService.GetStatuses();
            if (result.IsSucced)
            {
                return Ok(result.Message);
            }
            return NotFound();

            //var statuses = await _context.Statuses.Include(c => c.Client).Select(x => new StatusDTO
            //{
            //    Id= x.Id,
            //    Client = x.Client.Name,
            //    Search = x.AmountOfAccount,
            //    Deposit = x.AmountOfDeposit,
            //    Balance = x.Balance,
            //    ClientId= x.ClientId,
                
            //}).ToListAsync();

            //return Ok(statuses);
        }
    }
}
