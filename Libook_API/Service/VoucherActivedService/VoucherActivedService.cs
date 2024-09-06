using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.OrderDetailRepo;
using Libook_API.Repositories.VoucherActivedRepo;

namespace Libook_API.Service.VoucherActivedService
{
    public class VoucherActivedService : IVoucherActivedService
    {
        private readonly IVoucherActivedRepository voucherActivedRepository;
        private readonly IMapper mapper;

        public VoucherActivedService(IVoucherActivedRepository voucherActivedRepository, IMapper mapper)
        {
            this.voucherActivedRepository = voucherActivedRepository;
            this.mapper = mapper;
        }
        public async Task<VoucherActivedResponseDTO> AddVoucherAsync(VoucherActivedDTO voucherActivedDTO)
        {
            // Map or Convert DTO to Domain Model
            var voucherActivedDomain = mapper.Map<VoucherActived>(voucherActivedDTO);

            // Use Domain Model
            voucherActivedDomain = await voucherActivedRepository.InsertAsync(voucherActivedDomain);

            return mapper.Map<VoucherActivedResponseDTO>(voucherActivedDomain);
        }

        public async Task<IEnumerable<VoucherActivedResponseDTO?>> GetAllVoucherActivedAsync()
        {
            var voucherActivedDomains = await voucherActivedRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var voucherActivedResponses = mapper.Map<List<VoucherActivedResponseDTO>>(voucherActivedDomains);

            return voucherActivedResponses;
        }

        public async Task<VoucherActivedResponseDTO?> GetVoucherActivedByIdAsync(Guid voucherActivedId)
        {
            var voucherActivedDomain = await voucherActivedRepository.GetByIdAsync(voucherActivedId);
            return mapper.Map<VoucherActivedResponseDTO>(voucherActivedDomain);
        }

        public async Task<IEnumerable<VoucherActivedResponseDTO?>> GetVoucherActivedByVoucherIdAsync(Guid voucherId)
        {
            var voucherActivedDomains = await voucherActivedRepository.GetByVoucherId(voucherId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var voucherActivedResponses = mapper.Map<List<VoucherActivedResponseDTO>>(voucherActivedDomains);

            return voucherActivedResponses;
        }

        public async Task<bool> VoucherActivedAsync(Guid userId, Guid voucherId)
        {
            var voucherActivedDomain = await voucherActivedRepository.VoucherActivedAsync(userId, voucherId);
            if(voucherActivedDomain.Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
