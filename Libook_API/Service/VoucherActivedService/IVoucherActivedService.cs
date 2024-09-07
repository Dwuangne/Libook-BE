using Libook_API.Models.DTO;

namespace Libook_API.Service.VoucherActivedService
{
    public interface IVoucherActivedService
    {
        Task<Boolean> VoucherActivedAsync(Guid userId, Guid voucherId);
        Task<IEnumerable<VoucherActivedResponseDTO?>> GetAllVoucherActivedAsync();
        Task<VoucherActivedResponseDTO?> GetVoucherActivedByIdAsync(Guid voucherActivedId);
        Task<IEnumerable<VoucherActivedResponseDTO?>> GetVoucherActivedByVoucherIdAsync(Guid voucherId);
        Task<VoucherActivedResponseDTO> AddVoucherActivedAsync(VoucherActivedDTO voucherActivedDTO);
    }
}
