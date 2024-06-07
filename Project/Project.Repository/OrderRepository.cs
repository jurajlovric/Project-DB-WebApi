using Project.Common;
using Project.Model;
using System.Text;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Project.Repository.Common;

namespace Project.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<Order>> GetOrdersAsync(FilterParameters filter, SortParameters sort, PageParameters page)
        {
            var orders = new List<Order>();
            var query = new StringBuilder("SELECT * FROM \"Order\" WHERE 1=1");

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = conn;

                    if (filter.ConditionID.HasValue)
                    {
                        query.Append(" AND \"ConditionID\" = @ConditionID");
                        command.Parameters.AddWithValue("@ConditionID", filter.ConditionID.Value);
                    }
                    if (!string.IsNullOrEmpty(filter.StateID))
                    {
                        query.Append(" AND \"StateID\" = @StateID");
                        command.Parameters.AddWithValue("@StateID", filter.StateID);
                    }
                    if (filter.MinPrice.HasValue)
                    {
                        query.Append(" AND \"Price\" >= @MinPrice");
                        command.Parameters.AddWithValue("@MinPrice", filter.MinPrice.Value);
                    }
                    if (filter.MaxPrice.HasValue)
                    {
                        query.Append(" AND \"Price\" <= @MaxPrice");
                        command.Parameters.AddWithValue("@MaxPrice", filter.MaxPrice.Value);
                    }
                    if (filter.StartDate.HasValue)
                    {
                        query.Append(" AND \"OrderDate\" >= @StartDate");
                        command.Parameters.AddWithValue("@StartDate", filter.StartDate.Value);
                    }
                    if (filter.EndDate.HasValue)
                    {
                        query.Append(" AND \"OrderDate\" <= @EndDate");
                        command.Parameters.AddWithValue("@EndDate", filter.EndDate.Value);
                    }
                    if (!string.IsNullOrEmpty(filter.SearchQuarry))
                    {
                        query.Append(" AND (\"Price\"::text ILIKE @SearchQuarry OR \"StateID\" ILIKE @SearchQuarry)");
                        command.Parameters.AddWithValue("@SearchQuarry", "%" + filter.SearchQuarry + "%");
                    }

                    if (!string.IsNullOrEmpty(sort.OrderBy))
                    {
                        query.Append($" ORDER BY \"{sort.OrderBy}\" {(sort.SortOrder.ToLower() == "desc" ? "DESC" : "ASC")}");
                    }

                    query.Append(" OFFSET @Offset LIMIT @Limit");
                    command.Parameters.AddWithValue("@Offset", (page.PageNumber - 1) * page.PageSize);
                    command.Parameters.AddWithValue("@Limit", page.PageSize);

                    command.CommandText = query.ToString();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Order
                            {
                                Id = reader.GetGuid(0),
                                CustomerId = reader.GetGuid(1),
                                OrderDate = reader.GetDateTime(2),
                                Price = reader.GetDecimal(3),
                                StateID = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ConditionID = reader.IsDBNull(5) ? null : (Guid?)reader.GetGuid(5)
                            });
                        }
                    }
                }
            }

            return orders;
        }
    }
}
