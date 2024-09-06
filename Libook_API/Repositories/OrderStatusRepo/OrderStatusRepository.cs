using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Libook_API.Repositories.OrderStatusRepo
{
    public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderStatus>> GetByOrderId(Guid orderId)
        {
            return await _dbSet.Where(orderStatus => orderStatus.OrderId == orderId).ToListAsync();
        }
    }
}
