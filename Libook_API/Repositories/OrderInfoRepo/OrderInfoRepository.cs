using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Libook_API.Repositories.OrderInfoRepo
{
    public class OrderInfoRepository : GenericRepository<OrderInfo>, IOrderInfoRepository
    {
        public OrderInfoRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderInfo>> GetByUserId(Guid userId)
        {
            return await _dbSet
                .Where(orderInfo => orderInfo.UserId == userId).ToListAsync();
        }
    }
}
