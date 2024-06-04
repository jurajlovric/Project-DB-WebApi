using Project.Model;
using System;
using System.Collections.Generic;
using Npgsql;

namespace Project.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM \"Customer\"", conn))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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

        public Customer GetCustomerById(Guid id)
        {
            Customer customer = null;
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM \"Customer\" WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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

        public void AddCustomer(Customer customer)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(
                    "INSERT INTO \"Customer\" (\"Id\", \"FirstName\", \"LastName\", \"Email\") VALUES (@Id, @FirstName, @LastName, @Email)", conn))
                {
                    command.Parameters.AddWithValue("Id", customer.Id);
                    command.Parameters.AddWithValue("FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("LastName", customer.LastName);
                    command.Parameters.AddWithValue("Email", customer.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(
                    "UPDATE \"Customer\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Email\" = @Email WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("LastName", customer.LastName);
                    command.Parameters.AddWithValue("Email", customer.Email);
                    command.Parameters.AddWithValue("Id", customer.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(Guid id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("DELETE FROM \"Customer\" WHERE \"Id\" = @Id", conn))
                {
                    command.Parameters.AddWithValue("Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
