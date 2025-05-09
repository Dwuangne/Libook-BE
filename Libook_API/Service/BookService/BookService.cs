﻿using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.BookRepo;
using Libook_API.Repositories.VoucherRepo;
using Libook_API.Service.BookImageService;
using System.Linq.Expressions;

namespace Libook_API.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookImageService bookImageService;
        private readonly IMapper mapper;

        public BookService(IBookRepository bookRepository, IBookImageService bookImageService, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.bookImageService = bookImageService;
            this.mapper = mapper;
        }
        public async Task<BookResponseDTO> AddBookAsync(BookDTO bookDTO)
        {
            // Map or Convert DTO to Domain Model
            var bookDomain = mapper.Map<Book>(bookDTO);

            bookDomain.ImageUrl = bookDomain.BookImages.First().BookImageUrl;
            foreach (var bookImage in bookDomain.BookImages)
            {
                bookImage.BookId = bookDomain.Id;
            }

            // Use Domain Model to create Author
            bookDomain = await bookRepository.InsertAsync(bookDomain);

            return mapper.Map<BookResponseDTO>(bookDomain);
        }

        public async Task<BookListDTO?> GetBookAsync(
            Expression<Func<Book, bool>>? filter,
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy,
            string includeProperties,
            int pageIndex,
            int pageSize
            )
        {
            var response = await bookRepository.GetAsync(
                        filter: filter,
                        orderBy: orderBy,
                        includeProperties: includeProperties,
                        pageIndex: pageIndex,
                        pageSize: pageSize
                        );
            var bookDomains = response.Item1;
            var totalPage = response.Item2;

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var bookResponse = mapper.Map<List<BookResponseDTO>>(bookDomains);

            var bookListResponse = new BookListDTO
            {
                TotalPage = totalPage,
                PageIndex = pageIndex,
                BookResponseDTOs = bookResponse
            };

            return bookListResponse;
        }

        public async Task<BookResponseDTO?> GetBookByIdAsync(Guid bookId)
        {
            var bookDomain = await bookRepository.GetByIdAsync(bookId);
            return mapper.Map<BookResponseDTO>(bookDomain);
        }

        public async Task<BookResponseDTO?> UpdateBookAsync(Guid bookId, BookUpdateDTO bookUpdateDTO)
        {
            var existingBook = await bookRepository.GetByIdAsync(bookId);
            var bookImageResponse = await bookImageService.GetBookImageByBookIdAsync(bookId);
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Name = bookUpdateDTO.Name;
            existingBook.Description = bookUpdateDTO.Description;
            existingBook.Price = bookUpdateDTO.Price;
            existingBook.PrecentDiscount = bookUpdateDTO.PrecentDiscount;
            existingBook.Remain = bookUpdateDTO.Remain;
            existingBook.AuthorId = bookUpdateDTO.AuthorId;
            existingBook.CategoryId = bookUpdateDTO.CategoryId;
            existingBook.SupplierId = bookUpdateDTO.SupplierId;

            var bookImageDomain = await bookImageService.GetBookImageByBookImageUrlAsync(existingBook.Id, existingBook.ImageUrl);
            if (!bookImageDomain.Any())
            {
                existingBook.ImageUrl = bookImageResponse.First().BookImageUrl;
            }

            existingBook = await bookRepository.UpdateAsync(existingBook);

            return mapper.Map<BookResponseDTO>(existingBook);
        }

        public async Task<BookResponseDTO?> UpdateBookRemainAsync(Guid bookId, int bookRemain)
        {
            var existingBook = await bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Remain = bookRemain;

            existingBook = await bookRepository.UpdateAsync(existingBook);

            return mapper.Map<BookResponseDTO>(existingBook);
        }
    }
}
