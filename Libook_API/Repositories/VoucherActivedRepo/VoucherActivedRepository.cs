using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.VoucherActivedRepo
{
    public class VoucherActivedRepository : GenericRepository<VoucherActived>, IVoucherActivedRepository
    {
        public VoucherActivedRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
