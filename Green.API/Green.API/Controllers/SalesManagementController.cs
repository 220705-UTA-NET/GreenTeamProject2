using Green.API.Models;
using Green.Api.Data;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
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
        [HttpGet]
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

        [HttpGet]
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
        


        [HttpPost]
        public async Task<ActionResult> PostCustomer(string username, string password)
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertCustomerAsync(username,password); 
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
       
        [HttpPost]
        public async Task<ActionResult> PostSalesInvoice(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount)
        {
             try
            {
                StatusCodeResult rep = await _repo.InsertSalesInvoiceAsync(invoicedate,customerid,paymenttype,totalamount); 
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
        [HttpPost]
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