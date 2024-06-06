using Project.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Project.Repository.Common;

namespace Project.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = new List<Customer>();
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM \"Customer\"", conn))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3)
                        });
                    }
                }
            }
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            Customer customer = null;
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = new NpgsqlCommand("SELECT * FROM \"Customer\" WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            customer = new Customer
                            {
                                Id = reader.GetGuid(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return customer;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = new NpgsqlCommand(
                    "INSERT INTO \"Customer\" (\"Id\", \"FirstName\", \"LastName\", \"Email\") VALUES (@Id, @FirstName, @LastName, @Email)", conn))
                {
                    command.Parameters.AddWithValue("Id", customer.Id);
                    command.Parameters.AddWithValue("FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("LastName", customer.LastName);
                    command.Parameters.AddWithValue("Email", customer.Email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = new NpgsqlCommand(
                    "UPDATE \"Customer\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Email\" = @Email WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("LastName", customer.LastName);
                    command.Parameters.AddWithValue("Email", customer.Email);
                    command.Parameters.AddWithValue("Id", customer.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = new NpgsqlCommand("DELETE FROM \"Customer\" WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
