using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model;
using Project.Service.Common;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public OrdersController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
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

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync(
            [FromQuery] string stateId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] Guid? conditionId = null,
            [FromQuery] string searchQuarry = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string orderBy = "OrderDate",
            [FromQuery] string sortOrder = "asc")
        {
            var filter = new FilterParameters
            {
                StateID = stateId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                StartDate = startDate,
                EndDate = endDate,
                ConditionID = conditionId,
                SearchQuarry = searchQuarry
            };

            var sort = new SortParameters
            {
                OrderBy = orderBy,
                SortOrder = sortOrder
            };

            var page = new PageParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var orders = await _orderService.GetOrdersAsync(filter, sort, page);
            return Ok(orders);
        }
    }
}
