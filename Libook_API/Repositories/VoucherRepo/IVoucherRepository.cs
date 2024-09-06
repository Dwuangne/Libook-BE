using Libook_API.Models.Domain;

namespace Libook_API.Repositories.VoucherRepo
{
    public interface IVoucherRepository : IGenericRepository<Voucher>
    {
        Task<IEnumerable<Voucher>> GetVoucherValidAsync();
    }
}
