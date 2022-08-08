
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Green.API.Models;
using System.Data.SqlClient;



namespace Green.Api.Data
{
    public class SqlRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<SqlRepository> _logger;

        public SqlRepository(string connectionString, ILogger<SqlRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            List<Customer> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT username, password, email, name, address, phonenumber FROM Customers;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string username = reader.GetString(0);
                string password = reader.GetString(1);
                string email = reader.GetString(2);
                string name =  reader.GetString(3);
                string? address = reader.IsDBNull(4) ? "" : reader.GetString(4);
                int phonenumber = reader.GetInt32(5);


                Customer tmpCustomer = new Customer(username, password, email, name, address, phonenumber);
                result.Add(tmpCustomer);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllCustomersAsync, returned {0} results", result.Count);

            return result;
        }


        public async Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email)
        {
            string cmdText = "INSERT INTO Customer (username,password, email) VALUES (@username, @password, @email)";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);


            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                _logger.LogError("Error in InsertCustomer while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertCustomerAsync");
            return new StatusCodeResult(200);

        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            List<Product> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT category, product_name, description, Artists.artist_name, unit_price FROM Products JOIN Artist on Products.artist_id = Artists.artist_id;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string category =  reader.GetString(0);
                string productname = reader.GetString(1);
                string description =  reader.GetString(2);
                string artistname = reader.GetString(3);
                decimal unitprice = reader.GetDecimal(4);


                Product tmpProduct = new Product(category,productname, description, artistname, unitprice);
                result.Add(tmpProduct);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllProductsAsync, returned {0} results", result.Count);

            return result;
        }

        public async Task<IEnumerable<SalesInvoice>> GetAllSalesInvoicesAsync()
        {
            List<SalesInvoice> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT Customers.name, Customer.email, Customers.address, invoice_date, payment_type, total_amount FROM SalesInvoices JOIN Customers on Customers.customer_id = SalesInvoices.custormer_id;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string name =  reader.GetString(0);
                string email =  reader.GetString(1);
                string? address = reader.IsDBNull(2) ? "" : reader.GetString(2);
                DateTime invoicedate = reader.GetString(3);
                string paymenttype =  reader.GetString(4);
                decimal totalamount = reader.GetDecimal(5);


                SalesInvoice tmpSalesInvoice = new SalesInvoice(name,email,address,invoicedate,paymenttype,totalamount);
                result.Add(tmpSalesInvoice);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllSalesInvoicesAsync, returned {0} results", result.Count);

            return result;
        }
        public async Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount)
        {
            string cmdText = "INSERT INTO SalesInvoices ( invoice_date, customer_id, payment_type, total_amount) VALUES (@invoicedate, @customerid, @paymenttype, @totalamount)";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@invoicedate", invoicedate);
            cmd.Parameters.AddWithValue("@customerid", customerid);
            cmd.Parameters.AddWithValue("@paymenttype", paymenttype);
            cmd.Parameters.AddWithValue("@totalamount", totalamount);


            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                _logger.LogError("Error in InsertSalesInvoice while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertSalesInvoiceAsync");
            return new StatusCodeResult(200);

        }
        public async Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync()
        {
            List<SalesInvoice> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT Products.product_name,quantity, total_amount FROM InvoiceLines JOIN Products on Products.product_id = InvoiceLines.product_id;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string productname =  reader.GetString(0);
                int quantity = reader.GetInt32(1);
                decimal totalamount = reader.GetDecimal(2);

                InvoiceLine tmpInvoiceLine = new InvoiceLine(productname,quantity, totalamount);
                result.Add(tmpInvoiceLine);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllInvoiceLinessAsync, returned {0} results", result.Count);

            return result;
        }
        public async Task<StatusCodeResult> InsertInvoiceLineAsync(int productid, int quantity)
        {
            string cmdText = "INSERT INTO InvoiceLines ( product_id, quantity) VALUES (@productid, @quantity)";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@productid", productid);
            cmd.Parameters.AddWithValue("@quantity", quantity);


            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                _logger.LogError("Error in InsertInvoiceLine while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertInvoiceLineAsync");
            return new StatusCodeResult(200);
        }
    }
}
