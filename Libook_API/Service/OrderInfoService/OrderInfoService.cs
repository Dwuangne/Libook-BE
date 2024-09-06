using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.OrderInfoRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.SupplierRepo;

namespace Libook_API.Service.OrderInfoService
{
    public class OrderInfoService : IOrderInfoService
    {
        private readonly IOrderInfoRepository orderInfoRepository;
        private readonly IMapper mapper;

        public OrderInfoService(IOrderInfoRepository orderInfoRepository, IMapper mapper)
        {
            this.orderInfoRepository = orderInfoRepository;
            this.mapper = mapper;
        }
        public async Task<OrderInfoResponseDTO> AddOrderInfoAsync(OrderInfoDTO orderInfoDTO)
        {
            // Map or Convert DTO to Domain Model
            var orderInfoDomain = mapper.Map<OrderInfo>(orderInfoDTO);

            // Use Domain Model
            orderInfoDomain = await orderInfoRepository.InsertAsync(orderInfoDomain);

            return mapper.Map<OrderInfoResponseDTO>(orderInfoDomain);
        }

        public async Task<IEnumerable<OrderInfoResponseDTO?>> GetAllOrderInfoAsync()
        {
            var orderInfoDomains = await orderInfoRepository.GetAllAsync();

            var orderInfoResponse = mapper.Map<List<OrderInfoResponseDTO>>(orderInfoDomains);

            return orderInfoResponse;
        }

        public async Task<OrderInfoResponseDTO?> GetOrderInfoByIdAsync(Guid orderInfoId)
        {
            var orderInfoDomain = await orderInfoRepository.GetByIdAsync(orderInfoId);
            return mapper.Map<OrderInfoResponseDTO>(orderInfoDomain);
        }

        public async Task<IEnumerable<OrderInfoResponseDTO?>> GetOrderInfoByUserIdAsync(Guid userId)
        {
            var orderInfoDomains = await orderInfoRepository.GetByUserId(userId);

            var orderInfoResponse = mapper.Map<List<OrderInfoResponseDTO>>(orderInfoDomains);

            return orderInfoResponse;
        }

        public async Task<OrderInfoResponseDTO?> UpdateOrderInfoAsync(Guid orderInfoId, OrderInfoUpdateDTO orderInfoUpdateDTO)
        {
            var existingOrderInfo = await orderInfoRepository.GetByIdAsync(orderInfoId);
            if (existingOrderInfo == null)
            {
                return null;
            }

            existingOrderInfo.Name = orderInfoUpdateDTO.Name;
            existingOrderInfo.Phone = orderInfoUpdateDTO.Phone;
            existingOrderInfo.ProvinceId = orderInfoUpdateDTO.ProvinceId;
            existingOrderInfo.DistrictId = orderInfoUpdateDTO.DistrictId;
            existingOrderInfo.WardId = orderInfoUpdateDTO.WardId;
            existingOrderInfo.Address = orderInfoUpdateDTO.Address;

            existingOrderInfo = await orderInfoRepository.UpdateAsync(existingOrderInfo);

            return mapper.Map<OrderInfoResponseDTO>(existingOrderInfo);
        }
    }
}
