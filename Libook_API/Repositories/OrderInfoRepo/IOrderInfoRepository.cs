using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderInfoRepo
{
    public interface IOrderInfoRepository : IGenericRepository<OrderInfo>
    {
        Task<IEnumerable<OrderInfo>> GetByUserId(Guid userId);
    }
}
