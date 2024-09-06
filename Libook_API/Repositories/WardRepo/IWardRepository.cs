using Libook_API.Models.Domain;

namespace Libook_API.Repositories.WardRepo
{
    public interface IWardRepository : IGenericRepository<Ward>
    {
        Task<IEnumerable<Ward>> GetByDistrictId(string districtCode);
    }
}
