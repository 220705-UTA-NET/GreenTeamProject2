using Green.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Green.Api.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email);
        Task<IEnumerable<Customer>> GetAllProductsAsync();
        Task<IEnumerable<SalesInvoice>> GetAllSalesInovicesAsync()
        Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount)

    }
}