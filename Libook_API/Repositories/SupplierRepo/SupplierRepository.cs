using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.SupplierRepo
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
