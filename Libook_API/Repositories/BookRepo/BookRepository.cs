using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.BookRepo
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
