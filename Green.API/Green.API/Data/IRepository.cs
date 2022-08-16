using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Green.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Green.Api.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
<<<<<<< HEAD
=======
        Task<StatusCodeResult> GetExistingCustomerAsync(string username, string password);
>>>>>>> origin/daniel
        Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<SalesInvoice>> GetAllSalesInvoicesAsync();
        Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount);
        Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync();
        Task<StatusCodeResult> InsertInvoiceLineAsync(int productid, int quantity);

    }
}