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
        //Task<StatusCodeResult> GetExistingCustomerAsync(string username, string password);
        Task<IEnumerable<Product>> GetCustomerInvoiceLinesAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetAProductAsync(int productid);
        Task<IEnumerable<SalesInvoice>> GetAllSalesInvoicesAsync();
        Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount);
        //Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email);
        Task<StatusCodeResult> InsertInvoiceLineAsync(int invoice_number, int productid, int quantity, decimal amount);
        Task<IEnumerable<Product>> GetProductsOfCategoryAsync(string category);

        Task<IEnumerable<Product>> LoginUserCartAsync(int id);
        Task<ActionResult<Customer>> SignupUserAsync(Customer customer);
        Task<int> postnumasync(string s);
        Task<ActionResult<Customer>> FindCustomerAsync(string token);



    }
}