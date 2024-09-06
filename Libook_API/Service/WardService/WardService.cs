using AutoMapper;
using Libook_API.Models.DTO;
using Libook_API.Repositories.WardRepo;

namespace Libook_API.Service.WardService
{
    public class WardService : IWardService
    {
        private readonly IWardRepository wardRepository;
        private readonly IMapper mapper;

        public WardService(IWardRepository wardRepository, IMapper mapper)
        {
            this.wardRepository = wardRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<WardResponseDTO?>> GetWardByDistrictIdAsync(string districtCode)
        {
            var wardDomains = await wardRepository.GetByDistrictId(districtCode);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var wardResponses = mapper.Map<List<WardResponseDTO>>(wardDomains);

            return wardResponses;
        }

        public async Task<WardResponseDTO?> GetWardByIdAsync(string wardCode)
        {
            var wardDomain = await wardRepository.GetByIdAsync(wardCode);
            return mapper.Map<WardResponseDTO>(wardDomain);
        }
    }
}
