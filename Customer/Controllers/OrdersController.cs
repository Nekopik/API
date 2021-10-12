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
    public class OrdersController : ControllerBase
    {
        private readonly APIDbContext _context;

        public OrdersController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomers()
        {
            var orders = await _context.Orders.ToListAsync();


            var orderDTO = new List<OrderDTO>();

            foreach (var item in orders)
            {
                var dto = new OrderDTO();

                dto.Id = item.Id;

                dto.OrderDate = item.OrderDate;

                dto.CustomerId = item.CustomerId;
                dto.Customer = item.Customer; // ??

                orderDTO.Add(dto);

            }

            return orderDTO;


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDetailsDTO>>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            var orderDetailsDto = new List<OrderDetailsDTO>();

            if (order == null)
            {
                return NotFound();
            }

            var dto = new OrderDetailsDTO();

            dto.Id = order.Id;

            dto.OrderDate = order.OrderDate;

            dto.Quantity = order.Quantity;

            dto.OrderPrice = order.OrderPrice;

            dto.CustomerId = order.CustomerId;

            dto.Customer = order.Customer;

            dto.CustomerId = order.ProductId;

           dto.Product = order.Product;


            orderDetailsDto.Add(dto);

            return orderDetailsDto;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
