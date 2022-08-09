using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Green.API.Models;
using Green.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Green.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesManagementController : ControllerBase
    {

       private readonly IRepository _repo;
       private readonly ILogger<SalesManagementController> _logger;

        // Constructor
        public SalesManagementController(IRepository repo, ILogger<SalesManagementController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult> GetExistingCustomer(string username, string password)
        {

            try
            {
                
                StatusCodeResult st = await _repo.GetExistingCustomerAsync(username, password);
                _logger.LogInformation(st.StatusCode.ToString());
                
                if (st.StatusCode != 200) return StatusCode(500, "User not found");

            }
            catch (Exception e)
            {

                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            _logger.LogInformation("Executed GetExistingCustomer");
            return StatusCode(200, "User found");
        }


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
        [HttpGet("getallproducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            IEnumerable<Product> products;

            try
            {
                products = await _repo.GetAllProductsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return products.ToList();

        }
        [HttpGet("getallsalesinvoices")]
        public async Task<ActionResult<IEnumerable<SalesInvoice>>> GetAllSalesInvoices()
        {
            IEnumerable<SalesInvoice> salesinvoices;

            try
            {
                salesinvoices = await _repo.GetAllSalesInvoicesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return salesinvoices.ToList();
        }

        [HttpGet("getallinvoiceslines")]
        public async Task<ActionResult<IEnumerable<InvoiceLine>>> GetAllInvoiceLines()
        {
            IEnumerable<InvoiceLine> invoicelines;

            try
            {
                invoicelines = await _repo.GetAllInvoiceLinesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return invoicelines.ToList();
        }
        


        [HttpPost("{username}/{password}/{email}")]
        public async Task<ActionResult> PostCustomer(string username, string password, string email)// [FromBody]
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertCustomerAsync(username,password, email); 
                if (rep.StatusCode == 500) return StatusCode(500, "Customer could not be inserted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertCustomer");
                _logger.LogError(e.Message);
                return StatusCode(500, "Customer could not be inserted!");
            }
            return StatusCode(200);
        }
       
        [HttpPost("{invoicedate}/{customerid}/{paymenttype}/{totalamount}")]
        public async Task<ActionResult> PostSalesInvoice(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount)
        {
             try
            {
                StatusCodeResult rep = await _repo.InsertSalesInvoiceAsync(invoicedate, customerid, paymenttype, totalamount); 
                if (rep.StatusCode == 500) return StatusCode(500, "SalesInvoice could not be inserted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertSalesInvoice");
                _logger.LogError(e.Message);
                return StatusCode(500, "SalesInvoice could not be inserted!");
            }
            return StatusCode(200);
        }

        [HttpPost("{productid}/{quantity}")]
        public async Task<ActionResult> PostInvoiceLine(int productid, int quantity)
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertInvoiceLineAsync(productid,quantity); 
                if (rep.StatusCode == 500) return StatusCode(500, "InvoiceLine could not be inserted!");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error encountered: connecting to database in InsertInvoiceLine");
                _logger.LogError(e.Message);
                return StatusCode(500, "InvoiceLine could not be inserted!");
            }
            return StatusCode(200);
        }
    }
}