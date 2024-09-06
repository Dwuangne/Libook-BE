using Libook_API.Models.DTO;

namespace Libook_API.Service.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDTO?>> GetAllOrderAsync();
        Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OrderResponseDTO?>> GetOrderByUserIdAsync(Guid userId);
        Task<OrderResponseDTO> AddOrderAsync(OrderDTO orderDTO);
    }
}
