using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Green.API.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;

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

        public async Task<StatusCodeResult> GetExistingCustomerAsync(string username)
        {
            _logger.LogInformation(username);
            using SqlConnection connection = new(_connectionString);

            await connection.OpenAsync();

            string cmdText = "SELECT * FROM Customers WHERE username=@Username;";

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@Username", username);
         

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            try
            {
                if (!await reader.ReadAsync()) return new StatusCodeResult(500);
            }
            catch (Exception e)
            {
                _logger.LogError("Error in GetExistingCustomerAsync while trying to open a connection or execute non query");
                _logger.LogError(e.Message);
                await connection.CloseAsync();
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed GetExistingCustomerAsync");
            return new StatusCodeResult(200);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            List<Customer> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT username, name, address, phone, email, password FROM Customers;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {

                string username = reader.GetString(0);
                string name = reader.GetString(1);
                string? address = reader.IsDBNull(2) ? "" : reader.GetString(2);
                string? phonenumber = reader.IsDBNull(3) ? "" : reader.GetString(3);
                string email = reader.GetString(4);
                string? password = reader.IsDBNull(5) ? "" : reader.GetString(5);

                Customer tmpCustomer = new(username, password, email, name, address, phonenumber);
                result.Add(tmpCustomer);

            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllCustomersAsync, returned {0} results", result.Count);

            return result;
        }


        //public async Task<StatusCodeResult> InsertCustomerAsync(string username, string password, string email)
        //{
        //    string cmdText = "INSERT INTO Customers (username, password, email) VALUES (@username, @password, @email)";
        //    SqlConnection connection = new(_connectionString);


        //    using SqlCommand cmd = new(cmdText, connection);
        //    cmd.Parameters.AddWithValue("@username", username);
        //    cmd.Parameters.AddWithValue("@password", password);
        //    cmd.Parameters.AddWithValue("@email", email);


        //    try
        //    {
        //        await connection.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //    catch (Exception e)
        //    {

        //        _logger.LogError("Error in InsertCustomer while trying to open a connection or execute non query");
        //        _logger.LogInformation(e.Message);
        //        return new StatusCodeResult(500);
        //    }

        //    await connection.CloseAsync();
        //    _logger.LogInformation("Executed InsertCustomerAsync");
        //    return new StatusCodeResult(200);

        //}
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            List<Product> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT product_name, description, category_id, unit_price, artist_id FROM Products;";

            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string productname = reader.GetString(0);
                string description = reader.GetString(1);
                string category = reader.GetString(2);
                decimal unitprice = reader.GetDecimal(3);
                string artistname = reader.GetString(4);

                Product tmpProduct = new(category, productname, description, artistname, unitprice);
                result.Add(tmpProduct);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllProductsAsync, returned {0} results", result.Count);

            return result;
        }
        public async Task<Product> GetAProductAsync(int productid)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();
            Product tmpProduct = new Product();
            string cmdText = "SELECT product_name, description, category_id, unit_price, artist_id FROM Products where product_id = @productid;";
            using SqlCommand cmd = new(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string productname = reader.GetString(0);
                string description = reader.GetString(1);
                string category = reader.GetString(2);
                decimal unitprice = reader.GetDecimal(3);
                string artistname = reader.GetString(4);

                tmpProduct = new(category, productname, description, artistname, unitprice);
                await connection.CloseAsync();

                _logger.LogInformation("Executed GetAProductAsync, returned a results");

                return tmpProduct;
                
            }
            return tmpProduct;
        }

        public async Task<IEnumerable<SalesInvoice>> GetAllSalesInvoicesAsync()
        {
            List<SalesInvoice> result = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT Customers.name, Customers.email, Customers.address, invoice_date, payment_type, total_amount FROM SalesInvoices JOIN Customers on Customers.customer_id = SalesInvoices.customer_id;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string name = reader.GetString(0);
                string email = reader.GetString(1);
                string? address = reader.IsDBNull(2) ? "" : reader.GetString(2);
                DateTime invoicedate = reader.GetDateTime(3);
                string paymenttype = reader.GetString(4);
                decimal totalamount = reader.GetDecimal(5);


                SalesInvoice tmpSalesInvoice = new(name, email, address, invoicedate, paymenttype, totalamount);
                result.Add(tmpSalesInvoice);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllSalesInvoicesAsync, returned {0} results", result.Count);

            return result;
        }
        public async Task<StatusCodeResult> InsertSalesInvoiceAsync(DateTime invoicedate, int customerid, string paymenttype, decimal totalamount)
        {
            string cmdText = "INSERT INTO SalesInvoices ( invoice_date, customer_id, payment_type, total_amount) VALUES (@invoicedate, @customerid, @paymenttype, @totalamount);";
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






        //public async Task<IEnumerable<InvoiceLine>> GetAllInvoiceLinesAsync()// doesn't get correct ammount
        //{
        //    List<InvoiceLine> result = new();

        //    using SqlConnection connection = new(_connectionString);
        //    await connection.OpenAsync();

        //    string cmdText = "SELECT Products.product_name, quantity, amount FROM InvoiceLines JOIN Products on Products.product_id = InvoiceLines.product_id;";

        //    using SqlCommand cmd = new(cmdText, connection);

        //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        //    while (await reader.ReadAsync())
        //    {
        //        string productname =  reader.GetString(0);
        //        int quantity = reader.GetInt32(1);
        //        decimal totalamount = reader.GetDecimal(2);

        //        InvoiceLine tmpInvoiceLine = new(productname, quantity, totalamount);

        //        result.Add(tmpInvoiceLine);
        //    }

        //    await connection.CloseAsync();

        //    _logger.LogInformation("Executed GetAllInvoiceLinessAsync, returned {0} results", result.Count);

        //    return result;
        //}


        public async Task<StatusCodeResult> InsertInvoiceLineAsync(int invoice_number, int productid, int quantity, decimal amount)
        {
            string cmdText = "INSERT INTO InvoiceLines (invoice_number, product_id, quantity, amount) VALUES (@invoice_number, @productid, @quantity, @amount)";
            SqlConnection connection = new(_connectionString);

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@productid", productid);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@invoice_number", invoice_number);
            cmd.Parameters.AddWithValue("@amount", amount);

            try
            {
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {

                // if error is that a violation of primary key error -> increment quantity -> return stat 200
                _logger.LogError("Error in InsertInvoiceLine while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertInvoiceLineAsync");
            return new StatusCodeResult(200);
        }

        public async Task<IEnumerable<Product>> GetProductsOfCategoryAsync(string category)
        {
            List<Product> result = new();

            string cmdText = "SELECT * FROM Products WHERE category_id=@category;";
            SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@category", category);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int productId = reader.GetInt32(0);
                string categoryId = reader.GetString(1);
                string productname = reader.GetString(2);
                string description = reader.GetString(3);
                string artistname = reader.GetString(4);
                decimal unitprice = reader.GetDecimal(5);

                Product tmpProduct = new(productId, category, productname, description, artistname, unitprice);
                result.Add(tmpProduct);
            }

        //    using SqlCommand cmd = new(cmdText, connection);

            _logger.LogInformation("Executed GetProductsOfCategoryAsync, returned {0} results", result.Count);

            return result;
        }

        public async Task<IEnumerable<Product>> LoginUserCartAsync(int id)
        {
            List<Product> result = new();

            string cmdText = "SELECT Products.product_id, Products.category_id, Products.product_name, Products.[description], Products.artist_id, Products.unit_price from Products join InvoiceLines on Products.product_id=InvoiceLines.product_id where customer_id=@customer_id;";
            SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            using SqlCommand cmd = new(cmdText, connection);
            //cmd.Parameters.AddWithValue("@email", customer.Email);
            //cmd.Parameters.AddWithValue("@token", customer.Token);
            cmd.Parameters.AddWithValue("@customer_id", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();


            while (await reader.ReadAsync())
            {
                int productId = reader.GetInt32(0);
                string categoryId = reader.GetString(1);
                string productname = reader.GetString(2);
                string description = reader.GetString(3);
                string artistname = reader.GetString(4);
                decimal unitprice = reader.GetDecimal(5);

                Product tmpProduct = new(productId, categoryId, productname, description, artistname, unitprice);
                result.Add(tmpProduct);
            }

            _logger.LogInformation("Executed LoginUserCartAsync, returned {0} results", result.Count);

            return result;
        }
    


        public async Task<ActionResult<Customer>> SignupUserAsync(Customer customer)
        {
            string cmdText = "SELECT * from Customer WHERE @username=username AND @password=password;";
            SqlConnection connection = new(_connectionString);

            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@username", customer.Username);
            cmd.Parameters.AddWithValue("@password", customer.Password);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            Customer? c;
            try
            {
                await connection.OpenAsync();
                if (await reader.ReadAsync())
                {
                    c = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(6), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                }
                else
                {
                    return new StatusCodeResult(500);
                }
            }
            catch (Exception e)
            {

                // if error is that a violation of primary key error -> increment quantity -> return stat 200
                _logger.LogError("Error in InsertInvoiceLine while trying to open a connection or execute non query");
                _logger.LogInformation(e.Message);
                return new StatusCodeResult(500);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed InsertInvoiceLineAsync");
            return c;
        }
    }
}