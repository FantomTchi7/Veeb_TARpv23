using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_TARpv23.Data;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            if (device.NextMaintenanceTime < DateTime.Now)
            {
                return BadRequest("Maintenance time cannot be in the past.");
            }

            if (device.RedisualValue > device.AcquisitionCost)
            {
                return BadRequest("Residual value cannot be greater than the acquisition cost.");
            }

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            if (device.NextMaintenanceTime < DateTime.Now)
            {
                return BadRequest("Maintenance time cannot be in the past.");
            }

            if (device.RedisualValue > device.AcquisitionCost)
            {
                return BadRequest("Residual value cannot be greater than the acquisition cost.");
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }

        [HttpGet("NeedsMaintenance")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevicesNeedingMaintenance()
        {
            var devices = await _context.Devices
                .Where(d => d.NextMaintenanceTime < DateTime.Now && d.IsActive)
                .ToListAsync();
            return Ok(devices);
        }

        [HttpGet("TotalAcquisitionCost")]
        public async Task<ActionResult<int>> GetTotalAcquisitionCost()
        {
            var totalCost = await _context.Devices.SumAsync(d => d.AcquisitionCost);
            return Ok(totalCost);
        }

        [HttpGet("TotalResidualValue")]
        public async Task<ActionResult<int>> GetTotalResidualValue()
        {
            var totalValue = await _context.Devices.SumAsync(d => d.RedisualValue);
            return Ok(totalValue);
        }

        [HttpGet("Inactive")]
        public async Task<ActionResult<IEnumerable<Device>>> GetInactiveDevices()
        {
            var devices = await _context.Devices.Where(d => !d.IsActive).ToListAsync();
            return Ok(devices);
        }

        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<Device>>> GetActiveDevices()
        {
            var devices = await _context.Devices.Where(d => d.IsActive).ToListAsync();
            return Ok(devices);
        }
    }
}