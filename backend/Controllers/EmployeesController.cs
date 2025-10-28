using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Veeb_TARpv23.Models;

namespace Veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromQuery] int page = 1)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://reqres.in/api/users?page={page}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve employees from the external source.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ReqResApiResponse>(content);
            
            var employees = new List<Employee>();
            foreach (var user in apiResponse.Data)
            {
                employees.Add(new Employee
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Position = "N/A", 
                    PhoneNumber = "N/A"
                });
            }

            return Ok(employees);
        }
    }

    public class ReqResApiResponse
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public List<ReqResUser> Data { get; set; }
    }

    public class ReqResUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}