using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderInfoRepo
{
    public class OrderInfoRepository : GenericRepository<OrderInfo>, IOrderInfoRepository
    {
        public OrderInfoRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
