using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.BookRepo
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        public BookRepository(LibookDbContext context) : base(context)
        {
        }

        public async override Task<Book?> GetByIdAsync(object id)
        {
            return await _context.Books
                .Include(b => b.BookImages) // Include the related BookImages
                .FirstOrDefaultAsync(b => b.Id == (Guid)id); // Assuming 'id' is of type Guid
        }

    }
}
