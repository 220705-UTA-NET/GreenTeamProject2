using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Green.Api.Data;
using Green.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Green.API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IRepository _repo;
        private readonly ILogger<UserController> _logger;

        // Constructor
        public UserController(IRepository repo, ILogger<UserController> logger)
        {
            _repo = repo;
            _logger = logger;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return Content("UserController");
        }

        [HttpPost]
        public async Task<int> postNum([FromBody] string s)
        {
            
            try
            {
                int i = await _repo.postnumasync(s);
                return i;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Errors posting number");
            }

            return -1;
        }


        //[HttpGet("{username}/{password}")]
        //public async Task<ActionResult> GetExistingCustomer(string username, string password)
        //{

        //    try
        //    {
        //        StatusCodeResult st = await _repo.GetExistingCustomerAsync(username, password);
        //        if (st.StatusCode != 200) return StatusCode(500, "User not found");

        //    }
        //    catch (Exception e)
        //    {

        //        _logger.LogError(e, e.Message);
        //        return StatusCode(500);
        //    }

        //    _logger.LogInformation("Executed GetExistingCustomer");
        //    return StatusCode(200, "User found");
        //}


        // Two ways to access the endpoint
        // [HttpGet("/getallcustomers")] -> http://localhost:9999/getallcustomers
        // [HttpGet("getallcustomers")]  -> http://localhost:9999/SalesManagement/getallcustomers

        [HttpGet("getallcustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            IEnumerable<Customer> customers;

            try
            {
                customers = await _repo.GetAllCustomersAsync();
                if (customers == null || !customers.Any()) return BadRequest(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return customers.ToList();

        }


        [HttpPost("login/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> LoginUserCart(int id)
        {
            IEnumerable<Product> result;

            try
            {
                result = await _repo.LoginUserCartAsync(id);
                if (result == null) return BadRequest(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return result.ToList();

        }

        [HttpPost("SignupUser")]
        public async Task<ActionResult<Customer>> SignupUser([FromBody] Customer c)
        {
            ActionResult<Customer> customer;
            _logger.LogInformation("Hit signup method in the api");
            try
            {
                customer = await _repo.SignupUserAsync(c);
                if (customer == null) return BadRequest(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return customer;

        }

    }
}

