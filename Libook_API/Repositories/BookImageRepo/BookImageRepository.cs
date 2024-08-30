using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.BookImageRepo
{
    public class BookImageRepository : GenericRepository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
