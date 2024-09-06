using Libook_API.Data;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.DistrictRepo
{
    public class DistricRepository : GenericRepository<District>, IDistrictRepository
    {
        public DistricRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<District>> GetByProvinceId(string provinceCode)
        {
            return await _dbSet.Where(district => district.ProvinceCode.Equals(provinceCode)).ToListAsync();
        }
    }
}
