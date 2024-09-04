using Libook_API.Models.Domain;

namespace Libook_API.Repositories.BookImageRepo
{
    public interface IBookImageRepository : IGenericRepository<BookImage>
    {
        Task<IEnumerable<BookImage>> GetByBookId(Guid bookId);
    }
}
