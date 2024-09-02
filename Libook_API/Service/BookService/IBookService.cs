using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using System.Linq.Expressions;

namespace Libook_API.Service.BookService
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDTO?>> GetBookAsync(
            Expression<Func<Book, bool>>? filter, 
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy, 
            string includeProperties, 
            int pageIndex, 
            int pageSize);
        Task<BookResponseDTO?> GetBookByIdAsync(Guid bookId);
        Task<BookResponseDTO> AddBookAsync(BookDTO bookDTO);
        Task<BookResponseDTO?> UpdateBookAsync(Guid bookId, BookDTO bookDTO);
    }
}
