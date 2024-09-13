using Libook_API.Models.DTO;

namespace Libook_API.Service.PaymentOrderService
{
    public interface IPaymentOrderService
    {
        Task<IEnumerable<PaymentOrderResponseDTO?>> GetAllPaymentOrderAsync();
        Task<PaymentOrderResponseDTO?> GetPaymentOrderByIdAsync(long paymentOrderId);
        Task<PaymentOrderResponseDTO> AddPaymentOrderAsync(PaymentOrderDTO paymentOrderDTO);
    }
}
