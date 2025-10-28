using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Data;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.Include(i => i.InvoiceLine).ThenInclude(il => il.Product).Include(i => i.PaymentStatus).Include(i => i.Customer).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.Include(i => i.InvoiceLine).ThenInclude(il => il.Product).Include(i => i.PaymentStatus).Include(i => i.Customer).FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("unpaid")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetUnpaidInvoices()
        {
            return await _context.Invoices.Include(i => i.PaymentStatus)
                                           .Where(i => i.PaymentStatus.Status == false)
                                           .ToListAsync();
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetOverdueInvoices()
        {
            return await _context.Invoices.Include(i => i.PaymentStatus)
                                           .Where(i => i.PaymentStatus.Status == false && i.PaymentStatus.DueDate < DateTime.Now)
                                           .ToListAsync();
        }

        [HttpGet("customer/{customerId}/unpaid")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetCustomerUnpaidInvoices(int customerId)
        {
            return await _context.Invoices.Include(i => i.PaymentStatus)
                                           .Where(i => i.CustomerId == customerId && i.PaymentStatus.Status == false)
                                           .ToListAsync();
        }

        [HttpGet("customer/{customerId}/overdue")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetCustomerOverdueInvoices(int customerId)
        {
            return await _context.Invoices.Include(i => i.PaymentStatus)
                                           .Where(i => i.CustomerId == customerId && i.PaymentStatus.Status == false && i.PaymentStatus.DueDate < DateTime.Now)
                                           .ToListAsync();
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetCustomerInvoices(int customerId)
        {
            return await _context.Invoices.Where(i => i.CustomerId == customerId).ToListAsync();
        }

        [HttpGet("customer/{customerId}/total")]
        public async Task<ActionResult<int>> GetCustomerInvoicesTotal(int customerId)
        {
            return await _context.Invoices.Where(i => i.CustomerId == customerId).SumAsync(i => i.TotalAmount);
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}