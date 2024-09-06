using Libook_API.Models.Domain;
using Libook_API.Models.DTO;

namespace Libook_API.Repositories.DistrictRepo
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        Task<IEnumerable<District>> GetByProvinceId(string provinceCode);
    }
}
