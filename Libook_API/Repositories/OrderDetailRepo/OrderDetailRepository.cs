using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderDetailRepo
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
