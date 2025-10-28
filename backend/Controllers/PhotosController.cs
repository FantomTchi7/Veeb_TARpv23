using Microsoft.AspNetCore.Mvc;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PhotosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");
            response.EnsureSuccessStatusCode();
            var photos = await response.Content.ReadFromJsonAsync<IEnumerable<Photo>>();
            return Ok(photos);
        }
    }
}