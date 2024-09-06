using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Libook_API.Repositories.WardRepo
{
    public class WardRepository : GenericRepository<Ward>, IWardRepository
    {
        public WardRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ward>> GetByDistrictId(string districtCode)
        {
            return await _dbSet.Where(ward => ward.DistrictCode.Equals(districtCode)).ToListAsync();
        }
    }
}
