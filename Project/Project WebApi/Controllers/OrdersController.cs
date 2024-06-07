using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Service.Common;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        public class GetOrderRest
        {
            public Guid Id { get; set; }
            public Guid CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal Price { get; set; }
            public string StateID { get; set; }
            public Guid? ConditionID { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderRest>>> GetOrdersAsync(
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
            var orderRests = _mapper.Map<IEnumerable<GetOrderRest>>(orders);
            return Ok(orderRests);
        }
    }
}
