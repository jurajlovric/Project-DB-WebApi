using AutoMapper;
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
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public class GetCustomerRest
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerRest>>> GetCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();
            var customerRests = _mapper.Map<IEnumerable<GetCustomerRest>>(customers);
            return Ok(customerRests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerRest>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerRest = _mapper.Map<GetCustomerRest>(customer);
            return Ok(customerRest);
        }
        public class PostCustomerRest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<GetCustomerRest>> PostCustomerAsync([FromBody] PostCustomerRest postCustomerRest)
        {
            if (postCustomerRest == null)
            {
                return BadRequest("Customer data is null");
            }

            var customer = _mapper.Map<Customer>(postCustomerRest);
            await _customerService.AddCustomerAsync(customer);
            var customerRest = _mapper.Map<GetCustomerRest>(customer);
            return Created(string.Empty, customerRest);
        }
        public class PutCustomerRest
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAsync(Guid id, [FromBody] PutCustomerRest putCustomerRest)
        {
            if (putCustomerRest == null)
            {
                return BadRequest("Customer data is null");
            }

            if (id != putCustomerRest.Id)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<Customer>(putCustomerRest);

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
        public class DeleteCustomerRest
        {
            public Guid Id { get; set; }
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
