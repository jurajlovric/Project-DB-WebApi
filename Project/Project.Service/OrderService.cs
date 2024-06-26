﻿using Project.Common;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(FilterParameters filter, SortParameters sort, PageParameters page)
        {
            return await _orderRepository.GetOrdersAsync(filter, sort, page);
        }
    }
}
