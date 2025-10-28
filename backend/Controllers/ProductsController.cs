using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Data;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (product.ExpirationTime < DateTime.Now)
            {
                return BadRequest("Expiration time cannot be in the past.");
            }

            if (product.Price <= 0)
            {
                return BadRequest("Price cannot be zero or negative.");
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
            if (product.ExpirationTime < DateTime.Now)
            {
                return BadRequest("Expiration time cannot be in the past.");
            }

            if (product.Price <= 0)
            {
                return BadRequest("Price cannot be zero or negative.");
            }

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

        [HttpGet("expired")]
        public async Task<ActionResult<IEnumerable<Product>>> GetExpiredProducts()
        {
            return await _context.Products.Where(p => p.ExpirationTime < DateTime.Now).ToListAsync();
        }

        [HttpGet("totalcost")]
        public async Task<ActionResult<int>> GetTotalCost()
        {
            return await _context.Products.SumAsync(p => p.Price * p.StockQuantity);
        }

        [HttpGet("expiredtotalcost")]
        public async Task<ActionResult<int>> GetExpiredTotalCost()
        {
            return await _context.Products.Where(p => p.ExpirationTime < DateTime.Now).SumAsync(p => p.Price * p.StockQuantity);
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<Product>>> GetInactiveProducts()
        {
            return await _context.Products.Where(p => !p.IsActive).ToListAsync();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts()
        {
            return await _context.Products.Where(p => p.IsActive).ToListAsync();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}