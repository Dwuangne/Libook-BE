using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.OrderDetailRepo;
using Libook_API.Repositories.VoucherActivedRepo;
using Libook_API.Repositories.VoucherRepo;
using Libook_API.Service.VoucherActivedService;

namespace Libook_API.Service.VoucherService
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository voucherRepository;
        private readonly IVoucherActivedService voucherActivedService;
        private readonly IMapper mapper;

        public VoucherService(IVoucherRepository voucherRepository, IVoucherActivedService voucherActivedService, IMapper mapper)
        {
            this.voucherRepository = voucherRepository;
            this.voucherActivedService = voucherActivedService;
            this.mapper = mapper;
        }
        public async Task<VoucherResponseDTO> AddVoucherAsync(VoucherDTO voucherDTO)
        {
            // Map or Convert DTO to Domain Model
            var voucherDomain = mapper.Map<Voucher>(voucherDTO);

            // Use Domain Model
            voucherDomain = await voucherRepository.InsertAsync(voucherDomain);

            return mapper.Map<VoucherResponseDTO>(voucherDomain);
        }

        public async Task<IEnumerable<VoucherResponseDTO?>> GetAllVoucherAsync()
        {
            var voucherDomains = await voucherRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var voucherResponse = mapper.Map<List<VoucherResponseDTO>>(voucherDomains);

            return voucherResponse;
        }

        public async Task<VoucherResponseDTO?> GetVoucherByIdAsync(Guid voucherId)
        {
            var voucherDomain = await voucherRepository.GetByIdAsync(voucherId);
            return mapper.Map<VoucherResponseDTO>(voucherDomain);
        }

        public async Task<IEnumerable<VoucherResponseDTO?>> GetVoucherValidAsync(Guid userId)
        {
            var voucherDomains = await voucherRepository.GetVoucherValidAsync();
            var voucherValidDomains = new List<Voucher>();
            foreach (var voucherDomain in voucherDomains)
            {
                if (!(await voucherActivedService.VoucherActivedAsync(userId, voucherDomain.VoucherId)))
                {
                    voucherValidDomains.Add(voucherDomain);
                }
            }
            var voucherResponse = mapper.Map<List<VoucherResponseDTO>>(voucherValidDomains);

            return voucherResponse;
        }

        public async Task<VoucherResponseDTO?> UpdateVoucherRemainAsync(Guid voucherId, int voucherRemain)
        {
            var existingVoucher = await voucherRepository.GetByIdAsync(voucherId);
            if (existingVoucher == null)
            {
                return null;
            }
            existingVoucher.Remain = voucherRemain;

            existingVoucher = await voucherRepository.UpdateAsync(existingVoucher);

            return mapper.Map<VoucherResponseDTO>(existingVoucher);
        }
    }
}
