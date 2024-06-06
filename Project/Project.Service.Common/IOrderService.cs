using Project.Common;
using Project.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(FilterParameters filter, SortParameters sort, PageParameters page);
    }
}
