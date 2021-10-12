using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CustomerAPI.Models;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly APIDbContext _context;

        public CustomersController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();


            var customersDto = new List<CustomerDTO>();

            foreach (var item in customers)
            {
                var dto = new CustomerDTO();

                dto.Id = item.Id;

                dto.Name = item.Name;

                customersDto.Add(dto);

            }

            return customersDto;


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CustomerDetailsDTO>>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            var customersDetailsDto = new List<CustomerDetailsDTO>();

            if (customer == null)
            {
                return NotFound();
            }

            var dto = new CustomerDetailsDTO();

            dto.Id = customer.Id;

            dto.Name = customer.Name;

            dto.CompanyName = customer.CompanyName;

            dto.Phone = customer.Phone;

            dto.Email = customer.Email;

            dto.Adress = customer.Adress;

            dto.Country = customer.Country;

            dto.State = customer.State;

            dto.City = customer.City;

            dto.Gender = customer.Gender;

            customersDetailsDto.Add(dto);

            return customersDetailsDto;
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
