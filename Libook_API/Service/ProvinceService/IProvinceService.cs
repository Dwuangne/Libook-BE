using Libook_API.Models.DTO;

namespace Libook_API.Service.ProvinceService
{
    public interface IProvinceService
    {
        Task<IEnumerable<ProvinceResponseDTO?>> GetAllProvinceAsync();
        Task<ProvinceResponseDTO?> GetProvinceByIdAsync(String provinceCode);
    }
}
