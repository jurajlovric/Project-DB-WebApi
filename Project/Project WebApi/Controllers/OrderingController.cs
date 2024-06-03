using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Project_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly string _connectionString;

        public OrdersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("customers")]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = new List<Customer>();
            try
            {
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(customers);
        }

        [HttpGet("customers/{id}")]
        public ActionResult<Customer> GetCustomer(Guid id)
        {
            Customer customer = null;
            try
            {
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost("customers")]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("customers/{id}")]
        public IActionResult PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            try
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
                        command.Parameters.AddWithValue("Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }

        [HttpDelete("customers/{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("DELETE FROM \"Customer\" WHERE \"Id\" = @Id", conn))
                    {
                        command.Parameters.AddWithValue("Id", id);
                        int affectedRows = command.ExecuteNonQuery();
                        if (affectedRows == 0)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }
    }
}
