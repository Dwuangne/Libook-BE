using AutoMapper;
using Libook_API.Controllers;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.CommentImageRepo;
using Libook_API.Repositories.OrderDetailRepo;

namespace Libook_API.Service.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IMapper mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.mapper = mapper;
        }
        public async Task<OrderDetailResponseDTO> AddOrderDetailAsync(OrderDetailDTO orderDetailDTO)
        {
            // Map or Convert DTO to Domain Model
            var orderDetailDomain = mapper.Map<OrderDetail>(orderDetailDTO);

            // Use Domain Model
            orderDetailDomain = await orderDetailRepository.InsertAsync(orderDetailDomain);

            return mapper.Map<OrderDetailResponseDTO>(orderDetailDomain);
        }

        public async Task<IEnumerable<OrderDetailResponseDTO?>> GetAllOrderDetailAsync()
        {
            var orderDetailDomains = await orderDetailRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderDetailResponse = mapper.Map<List<OrderDetailResponseDTO>>(orderDetailDomains);

            return orderDetailResponse;
        }

        public async Task<OrderDetailResponseDTO?> GetOrderDetailByIdAsync(Guid orderDetailId)
        {
            var orderDetailDomain = await orderDetailRepository.GetByIdAsync(orderDetailId);
            return mapper.Map<OrderDetailResponseDTO>(orderDetailDomain);
        }

        public async Task<IEnumerable<OrderDetailResponseDTO>?> GetOrderDetailByOrderIdAsync(Guid orderId)
        {
            var orderDetailDomains = await orderDetailRepository.GetByOrderId(orderId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var orderDetailResponse = mapper.Map<List<OrderDetailResponseDTO>>(orderDetailDomains);

            return orderDetailResponse;
        }
    }
}
