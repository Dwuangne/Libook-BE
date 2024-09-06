using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.VoucherRepo
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Voucher>> GetVoucherValidAsync()
        {
            return await _dbSet
                .Where(voucher => voucher.EndDate > DateTime.UtcNow).ToListAsync();
        }
    }
}
