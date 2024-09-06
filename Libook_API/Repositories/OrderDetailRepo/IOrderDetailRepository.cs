using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderDetailRepo
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> GetByOrderId(Guid orderId);
    }
}
