using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.PaymentOrderRepo;

namespace Libook_API.Service.PaymentOrderService
{
    public class PaymentOrderService : IPaymentOrderService
    {
        private readonly IPaymentOrderRepository paymentOrderRepository;
        private readonly IMapper mapper;

        public PaymentOrderService(IPaymentOrderRepository paymentOrderRepository, IMapper mapper)
        {
            this.paymentOrderRepository = paymentOrderRepository;
            this.mapper = mapper;
        }
        public async Task<PaymentOrderResponseDTO> AddPaymentOrderAsync(PaymentOrderDTO paymentOrderDTO)
        {
            // Map or Convert DTO to Domain Model
            var paymentOrderDomain = mapper.Map<PaymentOrder>(paymentOrderDTO);

            paymentOrderDomain.CreatedDate = DateTime.Now;  

            // Use Domain Model to create Author
            paymentOrderDomain = await paymentOrderRepository.InsertAsync(paymentOrderDomain);

            return mapper.Map<PaymentOrderResponseDTO>(paymentOrderDomain);
        }

        public async Task<IEnumerable<PaymentOrderResponseDTO?>> GetAllPaymentOrderAsync()
        {
            var paymentOrderDomains = await paymentOrderRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var paymentResponse = mapper.Map<List<PaymentOrderResponseDTO>>(paymentOrderDomains);

            return paymentResponse;
        }

        public async Task<PaymentOrderResponseDTO?> GetPaymentOrderByIdAsync(long paymentOrderId)
        {
            var paymentOrderDomain = await paymentOrderRepository.GetByIdAsync(paymentOrderId);
            return mapper.Map<PaymentOrderResponseDTO>(paymentOrderDomain);
        }
    }
}
