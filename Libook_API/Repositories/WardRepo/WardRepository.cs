using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.WardRepo
{
    public class WardRepository : GenericRepository<Ward>, IWardRepository
    {
        public WardRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
