using Microsoft.AspNetCore.Mvc;
using Project.Model;
using Project.Service;
using Project.Repository;


namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public OrdersController()
        {
            string connectionString = "Host=localhost;Port=5432;Database=Order;User Id=postgres;Password=postgres";
            ICustomerRepository customerRepository = new CustomerRepository(connectionString);
            _customerService = new CustomerService(customerRepository);
        }

        [HttpGet("customers")]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return Ok(_customerService.GetCustomers());
        }

        [HttpGet("customers/{id}")]
        public ActionResult<Customer> GetCustomer(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("customers")]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("customers/{id}")]
        public IActionResult PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpDelete("customers/{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
