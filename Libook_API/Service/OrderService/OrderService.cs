using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.OrderDetailRepo;
using Libook_API.Repositories.OrderInfoRepo;
using Libook_API.Repositories.OrderRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.VoucherActivedRepo;
using Libook_API.Repositories.VoucherRepo;

namespace Libook_API.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public Task<OrderResponseDTO> AddOrderAsync(OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderResponseDTO?>> GetAllOrderAsync()
        {
            var orderDomains = await orderRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderResponse = mapper.Map<List<OrderResponseDTO>>(orderDomains);

            return orderResponse;
        }

        public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId)
        {
            var orderDomain = await orderRepository.GetByIdAsync(orderId);
            return mapper.Map<OrderResponseDTO>(orderDomain);
        }

        public async Task<IEnumerable<OrderResponseDTO?>> GetOrderByUserIdAsync(Guid userId)
        {
            var orderDomains = await orderRepository.GetByUserId(userId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderResponse = mapper.Map<List<OrderResponseDTO>>(orderDomains);

            return orderResponse;
        }
    }
}
