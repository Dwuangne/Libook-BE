using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderRepo
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
    }
}
