using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price = _toode.Price + 1;
            return _toode;
        }
    }
}
