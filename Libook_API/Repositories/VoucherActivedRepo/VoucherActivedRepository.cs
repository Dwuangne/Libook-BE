using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.VoucherActivedRepo
{
    public class VoucherActivedRepository : GenericRepository<VoucherActived>, IVoucherActivedRepository
    {
        public VoucherActivedRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VoucherActived>> GetByVoucherId(Guid voucherId)
        {
            return await _dbSet.Where(voucherActived => voucherActived.VoucherId == voucherId).ToListAsync();
        }

        public async Task<IEnumerable<VoucherActived>> VoucherActivedAsync(Guid userId, Guid voucherId)
        {
            return await _dbSet.Where(voucherActived => voucherActived.VoucherId == voucherId && voucherActived.UserId == userId).ToListAsync();
        }
    }
}
