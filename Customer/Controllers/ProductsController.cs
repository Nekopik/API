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
    public class ProductsController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ProductsController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();


            var productDTO = new List<ProductDTO>();

            foreach (var item in products)
            {
                var dto = new ProductDTO();

                dto.Id = item.Id;

                dto.Name = item.Name;

                productDTO.Add(dto);

            }

            return productDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDetailsDTO>>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            var productDetailsDto = new List<ProductDetailsDTO>();

            if (product == null)
            {
                return NotFound();
            }

            var dto = new ProductDetailsDTO();

            dto.Id = product.Id;

            dto.Name = product.Name;

            dto.Price = product.Price;

            dto.Currency = product.Currency;

            productDetailsDto.Add(dto);

            return productDetailsDto;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
