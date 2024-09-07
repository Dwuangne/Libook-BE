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

        public async override Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbSet
                .Include(order => order.OrderDetails)
                .Include(order => order.OrderStatuses).ToListAsync();
        }

        public async override Task<Order?> GetByIdAsync(object id)
        {
            return await _dbSet
                .Include(order => order.OrderDetails)
                .Include(order => order.OrderStatuses)
                .FirstOrDefaultAsync(order => order.OrderId.Equals(id));
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Include(order => order.OrderDetails)
                .Include(order => order.OrderStatuses)
                .Where(order => order.UserId == userId).ToListAsync();
        }


    }
}
