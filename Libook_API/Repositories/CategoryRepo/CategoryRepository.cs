using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
