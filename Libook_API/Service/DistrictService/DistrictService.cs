using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.DistrictRepo;
using Libook_API.Repositories.ProvinceRepo;

namespace Libook_API.Service.DistrictService
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository districtRepository;
        private readonly IMapper mapper;

        public DistrictService(IDistrictRepository districtRepository, IMapper mapper)
        {
            this.districtRepository = districtRepository;
            this.mapper = mapper;
        }
        public async Task<DistrictResponseDTO?> GetDistrictByIdAsync(string districtCode)
        {
            var districtDomain = await districtRepository.GetByIdAsync(districtCode);
            return mapper.Map<DistrictResponseDTO>(districtDomain);
        }

        public async Task<IEnumerable<DistrictResponseDTO?>> GetDistrictByProvinceIdAsync(String provinceCode)
        {
            var districtDomains = await districtRepository.GetByProvinceId(provinceCode);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var districtResponses = mapper.Map<List<DistrictResponseDTO>>(districtDomains);

            return districtResponses;
        }
    }
}
