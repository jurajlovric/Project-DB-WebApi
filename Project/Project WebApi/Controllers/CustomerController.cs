using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Service.Common;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersAsync()
        {
            return Ok(await _customerService.GetCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            await _customerService.AddCustomerAsync(customer);
            return Created(string.Empty, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAsync(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            try
            {
                await _customerService.UpdateCustomerAsync(customer);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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
