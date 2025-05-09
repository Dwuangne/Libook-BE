﻿using Libook_API.Models.Domain;

namespace Libook_API.Repositories.OrderStatusRepo
{
    public interface IOrderStatusRepository : IGenericRepository<OrderStatus>
    {
        Task<IEnumerable<OrderStatus>> GetByOrderId(Guid orderId);
    }
}
