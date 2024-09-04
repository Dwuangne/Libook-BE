using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.BookImageRepo;

namespace Libook_API.Service.BookImageService
{
    public class BookImageService : IBookImageService
    {
        private readonly IBookImageRepository bookImageRepository;
        private readonly IMapper mapper;

        public BookImageService(IBookImageRepository bookImageRepository, IMapper mapper)
        {
            this.bookImageRepository = bookImageRepository;
            this.mapper = mapper;
        }
        public async Task<BookImageResponseDTO> AddBookImageAsync(BookImageDTO bookImageDTO)
        {
            // Map or Convert DTO to Domain Model
            var bookImageDomain = mapper.Map<BookImage>(bookImageDTO);

            // Use Domain Model
            bookImageDomain = await bookImageRepository.InsertAsync(bookImageDomain);

            return mapper.Map<BookImageResponseDTO>(bookImageDomain);
        }

        public async Task<BookImageResponseDTO?> DeleteBookImageAsync(Guid bookImageId)
        {
            var existingBookImage = await bookImageRepository.GetByIdAsync(bookImageId);
            if (existingBookImage == null)
            {
                return null;
            }
            var bookImageDomain = await bookImageRepository.DeleteAsync(bookImageId);

            return mapper.Map<BookImageResponseDTO?>(bookImageDomain);
        }

        public async Task<IEnumerable<BookImageResponseDTO?>> GetAllBookImageAsync()
        {
            var bookImageDomain = await bookImageRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var bookResponse = mapper.Map<List<BookImageResponseDTO>>(bookImageDomain);

            return bookResponse;
        }

        public async Task<IEnumerable<BookImageResponseDTO?>> GetBookImageByBookIdAsync(Guid bookId)
        {
            var bookImageDomain = await bookImageRepository.GetByBookId(bookId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var bookResponse = mapper.Map<List<BookImageResponseDTO>>(bookImageDomain);

            return bookResponse;
        }

        public async Task<BookImageResponseDTO?> GetBookImageByIdAsync(Guid bookImageId)
        {
            var bookImageDomain = await bookImageRepository.GetByIdAsync(bookImageId);
            return mapper.Map<BookImageResponseDTO>(bookImageDomain);
        }

        public async Task<BookImageResponseDTO?> UpdateBookImageAsync(Guid bookImageId, BookImageUpdateDTO bookImageUpdateDTO)
        {
            var existingBookImage = await bookImageRepository.GetByIdAsync(bookImageId);
            if (existingBookImage == null)
            {
                return null;
            }

            existingBookImage.BookImageUrl = bookImageUpdateDTO.BookImageUrl;

            existingBookImage = await bookImageRepository.UpdateAsync(existingBookImage);

            return mapper.Map<BookImageResponseDTO>(existingBookImage);
        }
    }
}
