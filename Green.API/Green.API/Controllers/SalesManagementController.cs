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

        [HttpGet]
        public ActionResult Index()
        {
            return Content("Connected to SalesManagement Controller");
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

        //[HttpGet("getallinvoiceslines")]
        //public async Task<ActionResult<IEnumerable<InvoiceLine>>> GetAllInvoiceLines()
        //{
        //    IEnumerable<InvoiceLine> invoicelines;

        //    try
        //    {
        //        invoicelines = await _repo.GetAllInvoiceLinesAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, e.Message);
        //        return StatusCode(500);
        //    }

        //    return invoicelines.ToList();
        //}


        // this is wrong
        [HttpPost("{username}/{password}/{email}")]
        public async Task<ActionResult> PostCustomer(string username, string password, string email)// [FromBody]
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertCustomerAsync(username, password, email);
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


        [HttpPost("insertinvoice")]
        public async Task<ActionResult> PostInvoiceLine([FromBody] InvoiceLine invoice)
        {
            try
            {
                StatusCodeResult rep = await _repo.InsertInvoiceLineAsync(invoice.InvoiceNumber, invoice.ProductId, invoice.Quantity, invoice.Amount);
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

        [HttpGet("products/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsOfCategory(string category)
        {

            IEnumerable<Product> products;

            try
            {
                products = await _repo.GetProductsOfCategoryAsync(category);
                if (products == null || !products.Any()) return BadRequest(500);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return products.ToList();


        }
    }
}