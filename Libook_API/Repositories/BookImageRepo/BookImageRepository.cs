using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.BookImageRepo
{
    public class BookImageRepository : GenericRepository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookImage>> GetByBookId(Guid bookId)
        {
            return await _dbSet.Where(bookImage => bookImage.BookId == bookId).ToListAsync();
        }
    }
}
