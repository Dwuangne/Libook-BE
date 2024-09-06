using Libook_API.Models.Domain;
using System.Threading.Tasks;

namespace Libook_API.Repositories.VoucherActivedRepo
{
    public interface IVoucherActivedRepository : IGenericRepository<VoucherActived>
    {
        Task<IEnumerable<VoucherActived>> VoucherActivedAsync(Guid userId, Guid voucherId);
        Task<IEnumerable<VoucherActived>> GetByVoucherId(Guid voucherId);
    }
}
