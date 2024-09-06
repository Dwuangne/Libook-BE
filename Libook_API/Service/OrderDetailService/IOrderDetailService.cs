using Libook_API.Models.DTO;

namespace Libook_API.Service.OrderDetailService
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailResponseDTO?>> GetAllOrderDetailAsync();
        Task<OrderDetailResponseDTO?> GetOrderDetailByIdAsync(Guid orderDetailId);
        Task<IEnumerable<OrderDetailResponseDTO>?> GetOrderDetailByOrderIdAsync(Guid orderId);
        Task<OrderDetailResponseDTO> AddOrderDetailAsync(OrderDetailDTO orderDetailDTO);
    }
}
