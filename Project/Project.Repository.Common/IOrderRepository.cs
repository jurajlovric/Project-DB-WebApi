using Project.Common;
using Project.Model;

namespace Project.Repository.Common
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(FilterParameters filter, SortParameters sort, PageParameters page);
    }
}