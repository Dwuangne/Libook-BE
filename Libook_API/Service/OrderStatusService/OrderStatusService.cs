using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.CommentImageRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.SupplierRepo;

namespace Libook_API.Service.OrderStatusService
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IMapper mapper;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper)
        {
            this.orderStatusRepository = orderStatusRepository;
            this.mapper = mapper;
        }
        public async Task<OrderStatusResponseDTO> AddOrderStatusAsync(OrderStatusDTO orderStatusDTO)
        {
            // Map or Convert DTO to Domain Model
            var orderStatusDomain = mapper.Map<OrderStatus>(orderStatusDTO);

            orderStatusDomain.DateCreate = DateTime.Now;    

            // Use Domain Model
            orderStatusDomain = await orderStatusRepository.InsertAsync(orderStatusDomain);

            return mapper.Map<OrderStatusResponseDTO>(orderStatusDomain);
        }

        public async Task<IEnumerable<OrderStatusResponseDTO?>> GetAllOrderStatusAsync()
        {
            var orderStatusDomains = await orderStatusRepository.GetAllAsync();

            var orderStatusResponse = mapper.Map<List<OrderStatusResponseDTO>>(orderStatusDomains);

            return orderStatusResponse;
        }

        public async Task<OrderStatusResponseDTO?> GetOrderStatusByIdAsync(Guid orderStatusId)
        {
            var orderStatusDomain = await orderStatusRepository.GetByIdAsync(orderStatusId);
            return mapper.Map<OrderStatusResponseDTO>(orderStatusDomain);
        }

        public async Task<IEnumerable<OrderStatusResponseDTO?>> GetOrderStatusByOrderIdAsync(Guid orderId)
        {
            var orderStatusDomains = await orderStatusRepository.GetByOrderId(orderId);

            var orderStatusResponse = mapper.Map<List<OrderStatusResponseDTO>>(orderStatusDomains);

            return orderStatusResponse;
        }
    }
}
