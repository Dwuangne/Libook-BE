using Libook_API.Models.DTO;

namespace Libook_API.Service.BookImageService
{
    public interface IBookImageService
    {
        Task<IEnumerable<BookImageResponseDTO?>> GetAllBookImageAsync();
        Task<BookImageResponseDTO?> GetBookImageByIdAsync(Guid bookImageId);

        Task<IEnumerable<BookImageResponseDTO?>> GetBookImageByBookImageUrlAsync(Guid bookId, String bookImageurl);
        Task<IEnumerable<BookImageResponseDTO>?> GetBookImageByBookIdAsync(Guid bookId);
        Task<BookImageResponseDTO> AddBookImageAsync(BookImageDTO bookImageDTO);
        Task<BookImageResponseDTO?> UpdateBookImageAsync(Guid bookImageId, BookImageUpdateDTO bookImageUpdateDTO);
        Task<BookImageResponseDTO?> DeleteBookImageAsync(Guid bookImageId);
    }
}
