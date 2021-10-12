using CustomerAPI.Contracts;
using CustomerAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaportController : ControllerBase
    {
        private readonly IRaportRepository _raportRepo;
        public RaportController(IRaportRepository raportRepo)
        {
            _raportRepo = raportRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetRaport()
        {
            try
            {
                var raports = await _raportRepo.GetRaport();
                return Ok(raports);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
