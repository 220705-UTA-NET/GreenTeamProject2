
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

            string cmdText = "SELECT username, password, email, name, address, city, state, country phonenumber FROM Customers;";


            using SqlCommand cmd = new(cmdText, connection);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string username = reader.IsDBNull(0) ? "" : reader.GetString(0);
                string? password = reader.IsDBNull(1) ? "" : reader.GetString(1);
                string? email = reader.IsDBNull(2) ? "" : reader.GetString(2);
                string? name = reader.IsDBNull(3) ? "" : reader.GetString(3);
                string? address = reader.IsDBNull(4) ? "" : reader.GetString(4);
                int phonenumber = reader.GetInt32(5);


                Customer tmpCustomer = new Customer(username, password, email, name, address, phonenumber);
                result.Add(tmpCustomer);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetAllCustomersAsync, returned {0} results", result.Count);

            return result;
        }


        public async Task<StatusCodeResult> InsertCustomerAsync(string username, string password)
        {
            string cmdText = "INSERT INTO Customer (username,password) VALUES (@username, @password)";
            SqlConnection connection = new(_connectionString);


            using SqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@th", password);


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
    }
}
