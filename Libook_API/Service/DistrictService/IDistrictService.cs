using Libook_API.Models.DTO;

namespace Libook_API.Service.DistrictService
{
    public interface IDistrictService
    {
        Task<IEnumerable<DistrictResponseDTO?>> GetDistrictByProvinceIdAsync(string provinceCode);
        Task<DistrictResponseDTO?> GetDistrictByIdAsync(string districtCode);
    }
}
