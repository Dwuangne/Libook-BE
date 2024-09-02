using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.CategoryRepo;

namespace Libook_API.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryResponseDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var categoryDomain = mapper.Map<Category>(categoryDTO);

            categoryDomain = await categoryRepository.InsertAsync(categoryDomain);

            return mapper.Map<CategoryResponseDTO>(categoryDomain);
        }

        public async Task<IEnumerable<CategoryResponseDTO?>> GetAllCategoryAsync()
        {
            var categoryDomains = await categoryRepository.GetAllAsync();

            var categoryResponse = mapper.Map<List<CategoryResponseDTO>>(categoryDomains);

            return categoryResponse;
        }

        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId)
        {
            var categoryDomain = await categoryRepository.GetByIdAsync(categoryId);
            return mapper.Map<CategoryResponseDTO>(categoryDomain);
        }

        public async Task<CategoryResponseDTO?> UpdateCategoryAsync(Guid categoryId, CategoryDTO categoryDTO)
        {
            var existingCategory = await categoryRepository.GetByIdAsync(categoryId);
            
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = categoryDTO.Name;

            existingCategory = await categoryRepository.UpdateAsync(existingCategory);

            return mapper.Map<CategoryResponseDTO>(existingCategory);
        }
    }
}
