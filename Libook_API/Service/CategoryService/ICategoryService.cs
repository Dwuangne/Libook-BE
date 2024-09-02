using Libook_API.Models.DTO;

namespace Libook_API.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO?>> GetAllCategoryAsync();
        Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid categoryId);
        Task<CategoryResponseDTO> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<CategoryResponseDTO?> UpdateCategoryAsync(Guid categoryId, CategoryDTO categoryDTO);
    }
}
