using Green.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Green.Api.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<SalesInvoice>> GetAllSalesInvoicesAsync();
        Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount);
        Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync();
        Task<StatusCodeResult> InsertInvoiceLineAsync(int productid, int quantity);

    }
}