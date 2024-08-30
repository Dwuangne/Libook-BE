using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.ProvinceRepo
{
    public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
