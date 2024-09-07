using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.OrderInfoRepo;
using Libook_API.Repositories.OrderStatusRepo;
using Libook_API.Repositories.SupplierRepo;
using Libook_API.Service.DistrictService;
using Libook_API.Service.ProvinceService;
using Libook_API.Service.WardService;
using System.Net;
using System.Numerics;

namespace Libook_API.Service.OrderInfoService
{
    public class OrderInfoService : IOrderInfoService
    {
        private readonly IOrderInfoRepository orderInfoRepository;
        private readonly IProvinceService provinceService;
        private readonly IDistrictService districtService;
        private readonly IWardService wardService;
        private readonly IMapper mapper;

        public OrderInfoService(IOrderInfoRepository orderInfoRepository, IProvinceService provinceService, IDistrictService districtService, IWardService wardService, IMapper mapper)
        {
            this.orderInfoRepository = orderInfoRepository;
            this.provinceService = provinceService;
            this.districtService = districtService;
            this.wardService = wardService;
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

        public async Task<String> GetAddressAsync(Guid orderInfoId)
        {
            var orderInfoDomain = await orderInfoRepository.GetByIdAsync(orderInfoId);

            var provinceResponse = await provinceService.GetProvinceByIdAsync(orderInfoDomain.ProvinceId);
            var districtResponse = await districtService.GetDistrictByIdAsync(orderInfoDomain.DistrictId);
            var wardResponse = await wardService.GetWardByIdAsync(orderInfoDomain.WardId);
            return $"{orderInfoDomain.Name}|{orderInfoDomain.Phone}|{orderInfoDomain.Address}|{wardResponse.FullName}|{districtResponse.FullName}|{provinceResponse.FullName}";
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
