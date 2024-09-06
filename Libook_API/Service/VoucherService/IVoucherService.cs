using Libook_API.Models.DTO;

namespace Libook_API.Service.VoucherService
{
    public interface IVoucherService
    {
        Task<IEnumerable<VoucherResponseDTO?>> GetVoucherValidAsync(Guid userId);
        Task<IEnumerable<VoucherResponseDTO?>> GetAllVoucherAsync();
        Task<VoucherResponseDTO?> GetVoucherByIdAsync(Guid voucherId);
        Task<VoucherResponseDTO> AddVoucherAsync(VoucherDTO voucherDTO);
        Task<VoucherResponseDTO?> UpdateAuthorAsync(Guid voucherId, VoucherRemainUpdateDTO voucherRemainUpdateDTO);
    }
}
