using Libook_API.Models.DTO;

namespace Libook_API.Service.OrderInfoService
{
    public interface IOrderInfoService
    {
        Task<IEnumerable<OrderInfoResponseDTO?>> GetAllOrderInfoAsync();
        Task<OrderInfoResponseDTO?> GetOrderInfoByIdAsync(Guid orderInfoId);
        Task<IEnumerable<OrderInfoResponseDTO?>> GetOrderInfoByUserIdAsync(Guid userId);
        Task<String> GetAddressAsync(Guid orderInfoId);
        Task<OrderInfoResponseDTO> AddOrderInfoAsync(OrderInfoDTO orderInfoDTO);
        Task<OrderInfoResponseDTO?> UpdateOrderInfoAsync(Guid orderInfoId, OrderInfoUpdateDTO orderInfoUpdateDTO);
    }
}
