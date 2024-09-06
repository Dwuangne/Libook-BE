using AutoMapper;
using Libook_API.Models.DTO;
using Libook_API.Repositories.ProvinceRepo;
using Libook_API.Repositories.VoucherActivedRepo;

namespace Libook_API.Service.ProvinceService
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository provinceRepository;
        private readonly IMapper mapper;

        public ProvinceService(IProvinceRepository provinceRepository, IMapper mapper)
        {
            this.provinceRepository = provinceRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProvinceResponseDTO?>> GetAllProvinceAsync()
        {
            var provinceDomains = await provinceRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var voucherActivedResponses = mapper.Map<List<ProvinceResponseDTO>>(provinceDomains);

            return voucherActivedResponses;
        }

        public async Task<ProvinceResponseDTO?> GetProvinceByIdAsync(String provinceCode)
        {
            var provinceDomain = await provinceRepository.GetByIdAsync(provinceCode);
            return mapper.Map<ProvinceResponseDTO>(provinceDomain);
        }
    }
}
