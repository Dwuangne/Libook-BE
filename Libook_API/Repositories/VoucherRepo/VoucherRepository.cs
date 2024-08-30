using Libook_API.Data;
using Libook_API.Models.Domain;
namespace Libook_API.Repositories.VoucherRepo
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
