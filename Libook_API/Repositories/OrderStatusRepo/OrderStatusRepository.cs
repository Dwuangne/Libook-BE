using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderStatusRepo
{
    public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
