using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Libook_API.Repositories.OrderDetailRepo
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderId(Guid orderId)
        {
            return await _dbSet.Where(orderDetail => orderDetail.OrderId == orderId).ToListAsync();
        }
    }
}
