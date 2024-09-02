using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.SupplierRepo;

namespace Libook_API.Service.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;
        private readonly IMapper mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        public async Task<SupplierResponseDTO> AddSupplierAsync(SupplierDTO supplierDTO)
        {
            // Map or Convert DTO to Domain Model
            var supplierDomain = mapper.Map<Supplier>(supplierDTO);

            // Use Domain Model to create Author
            supplierDomain = await supplierRepository.InsertAsync(supplierDomain);

            return mapper.Map<SupplierResponseDTO>(supplierDomain);
        }

        public async Task<IEnumerable<SupplierResponseDTO?>> GetAllSupplierAsync()
        {
            var supplierDomains = await supplierRepository.GetAllAsync();

            var supplierResponse = mapper.Map<List<SupplierResponseDTO>>(supplierDomains);

            return supplierResponse;
        }

        public async Task<SupplierResponseDTO?> GetSupplierByIdAsync(Guid supplierId)
        {
            var supplierDomain = await supplierRepository.GetByIdAsync(supplierId);
            return mapper.Map<SupplierResponseDTO>(supplierDomain);
        }

        public async Task<SupplierResponseDTO?> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO)
        {
            var existingSupplier = await supplierRepository.GetByIdAsync(supplierId);
            if (existingSupplier == null)
            {
                return null;
            }

            existingSupplier.Name = supplierDTO.Name;

            existingSupplier = await supplierRepository.UpdateAsync(existingSupplier);

            return mapper.Map<SupplierResponseDTO>(existingSupplier);
        }
    }
}
