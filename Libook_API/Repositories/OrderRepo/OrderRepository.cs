using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
