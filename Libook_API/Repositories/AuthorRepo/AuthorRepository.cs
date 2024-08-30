using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.AuthorRepo
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
