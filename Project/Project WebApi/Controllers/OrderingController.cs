using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Service;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public OrdersController()
        {
            string connectionString = "Host=localhost;Port=5432;Database=Order;User Id=postgres;Password=postgres;";
            ICustomerRepository customerRepository = new CustomerRepository(connectionString);
            _customerService = new CustomerService(customerRepository);
        }

        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync()
        {
            return Ok(await _customerService.GetCustomersAsync());
        }

        [HttpGet("customers/{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("customers")]
        public async Task<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer);
        }

        [HttpPut("customers/{id}")]
        public async Task<IActionResult> PutCustomerAsync(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("customers/{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
