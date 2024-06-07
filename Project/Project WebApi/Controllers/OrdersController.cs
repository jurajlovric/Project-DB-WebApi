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
            string stateId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            Guid? conditionId = null,
            string searchQuarry = null,
            int pageNumber = 1,
            int pageSize = 10,
            string orderBy = "OrderDate",
            string sortOrder = "asc")
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
