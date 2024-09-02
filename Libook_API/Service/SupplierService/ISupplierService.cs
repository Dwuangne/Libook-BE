using Libook_API.Models.DTO;

namespace Libook_API.Service.SupplierService
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierResponseDTO?>> GetAllSupplierAsync();
        Task<SupplierResponseDTO?> GetSupplierByIdAsync(Guid supplierId);
        Task<SupplierResponseDTO> AddSupplierAsync(SupplierDTO supplierDTO);
        Task<SupplierResponseDTO?> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO);
    }
}
