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

        [HttpGet("Outstanding")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllOutstandingInvoices()
        {
            var invoices = await _context.Invoices
                .Include(i => i.PaymentStatus)
                .Where(i => i.PaymentStatus.Status == false)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("Overdue")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllOverdueInvoices()
        {
            var invoices = await _context.Invoices
                .Include(i => i.PaymentStatus)
                .Where(i => i.PaymentStatus.Status == false && i.PaymentStatus.DueDate < DateTime.Now)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("Consumer/{consumerId}/Unpaid")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetConsumerUnpaidInvoices(int consumerId)
        {
            var invoices = await _context.Invoices
                .Include(i => i.PaymentStatus)
                .Where(i => i.ConsumerId == consumerId && i.PaymentStatus.Status == false)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("Consumer/{consumerId}/Overdue")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetConsumerOverdueInvoices(int consumerId)
        {
            var invoices = await _context.Invoices
                .Include(i => i.PaymentStatus)
                .Where(i => i.ConsumerId == consumerId &&
                            i.PaymentStatus.Status == false &&
                            i.PaymentStatus.DueDate < DateTime.Now)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("Consumer/{consumerId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAllConsumerInvoices(int consumerId)
        {
            var invoices = await _context.Invoices
                .Where(i => i.ConsumerId == consumerId)
                .ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("Consumer/{consumerId}/TotalAmount")]
        public async Task<ActionResult<int>> GetConsumerTotalAmount(int consumerId)
        {
            var totalAmount = await _context.Invoices
                .Where(i => i.ConsumerId == consumerId)
                .SumAsync(i => i.Amount);
            return Ok(totalAmount);
        }
    }
}