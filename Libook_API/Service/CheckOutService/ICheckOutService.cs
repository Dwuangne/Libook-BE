using Libook_API.Models.DTO;
using Net.payOS.Types;

namespace Libook_API.Service.CheckOutService
{
    public interface ICheckOutService
    {
        Task<CreatePaymentResult> CreatePaymentLink(CheckOutDTO checkOutDTO);
        Task<OrderStatusResponseDTO?> PaymentSuccess(long orderCode);
    }
}
