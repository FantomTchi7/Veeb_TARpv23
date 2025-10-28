using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Data;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceLinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceLine>>> GetInvoiceLines()
        {
            return await _context.InvoiceLines.Include(il => il.Product).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceLine>> GetInvoiceLine(int id)
        {
            var invoiceLine = await _context.InvoiceLines.Include(il => il.Product).FirstOrDefaultAsync(il => il.Id == id);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return invoiceLine;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceLine(int id, InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceLineExists(id))
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
        public async Task<ActionResult<InvoiceLine>> PostInvoiceLine(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Add(invoiceLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceLine", new { id = invoiceLine.Id }, invoiceLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceLine(int id)
        {
            var invoiceLine = await _context.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            _context.InvoiceLines.Remove(invoiceLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceLineExists(int id)
        {
            return _context.InvoiceLines.Any(e => e.Id == id);
        }
    }
}