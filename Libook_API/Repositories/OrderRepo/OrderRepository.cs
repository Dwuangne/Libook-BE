using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetByUserId(Guid userId)
        {
            return await _dbSet.Where(order => order.UserId == userId).ToListAsync();
        }
    }
}
