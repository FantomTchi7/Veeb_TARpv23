using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Data;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDetails>>> GetContactDetails()
        {
            return await _context.ContactDetailsSet.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDetails>> GetContactDetails(int id)
        {
            var contactDetails = await _context.ContactDetailsSet.FindAsync(id);

            if (contactDetails == null)
            {
                return NotFound();
            }

            return contactDetails;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactDetails(int id, ContactDetails contactDetails)
        {
            if (id != contactDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDetailsExists(id))
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
        public async Task<ActionResult<ContactDetails>> PostContactDetails(ContactDetails contactDetails)
        {
            _context.ContactDetailsSet.Add(contactDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactDetails", new { id = contactDetails.Id }, contactDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactDetails(int id)
        {
            var contactDetails = await _context.ContactDetailsSet.FindAsync(id);
            if (contactDetails == null)
            {
                return NotFound();
            }

            _context.ContactDetailsSet.Remove(contactDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactDetailsExists(int id)
        {
            return _context.ContactDetailsSet.Any(e => e.Id == id);
        }
    }
}