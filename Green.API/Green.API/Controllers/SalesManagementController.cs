using Green.API.Models;
using Green.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Green.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesManagementController : ControllerBase
    {

        private readonly ILogger<SaleManagementController> _logger;

        public WeatherForecastController(ILogger<SaleManagementController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            IEnumerable<Customer> customers;

            try
            {
                customers = await _repo.GetAllCustomersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return customers.ToList();

        }


        [HttpPost]
        public async Task<ActionResult> PostCustomer(string username, string password)
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertCustomerAsync(username,password); 
                if (rep.StatusCode == 500) return StatusCode(500, "Monster could not be updated!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in UpdateMonster");
                _logger.LogError(e.Message);
                return StatusCode(500, "Monster could not be inserted!");
            }
            return StatusCode(200);
        }
    }
}