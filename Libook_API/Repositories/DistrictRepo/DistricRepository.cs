using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.DistrictRepo
{
    public class DistricRepository : GenericRepository<District>, IDistrictRepository
    {
        public DistricRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
