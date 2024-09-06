using Libook_API.Models.DTO;

namespace Libook_API.Service.WardService
{
    public interface IWardService
    {
        Task<IEnumerable<WardResponseDTO?>> GetWardByDistrictIdAsync(string districtCode);
        Task<WardResponseDTO?> GetWardByIdAsync(string wardCode);
    }
}
