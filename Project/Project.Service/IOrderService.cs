using Project.Common;
using Project.Model;

namespace Project.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(FilterParameters filter, SortParameters sort, PageParameters page);
    }
}
