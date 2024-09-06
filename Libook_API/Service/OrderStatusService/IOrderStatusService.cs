using Libook_API.Models.DTO;

namespace Libook_API.Service.OrderStatusService
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusResponseDTO?>> GetAllOrderStatusAsync();
        Task<OrderStatusResponseDTO?> GetOrderStatusByIdAsync(Guid orderStatusId);
        Task<IEnumerable<OrderStatusResponseDTO?>> GetOrderStatusByOrderIdAsync(Guid orderId);
        Task<OrderStatusResponseDTO> AddOrderStatusAsync(OrderStatusDTO orderStatusDTO);
    }
}
