using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? filter = null,
            string? authorId = null,
            string? categoryId = null,
            string? supplierId = null,
            string? orderBy = "Name",
            bool IsDescending = false,
            int pageIndex = 1,
            int pageSize = 12)
        {
            Expression<Func<Book, bool>> filterExpression = b => b.IsActive;

            // Example filter logic (customize as needed)
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = b => b.Name.Contains(filter);
            }

            if (!string.IsNullOrEmpty(authorId))
            {
                filterExpression = b => b.AuthorId.Equals(authorId);
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                filterExpression = b => b.CategoryId.Equals(categoryId);
            }

            if (!string.IsNullOrEmpty(supplierId))
            {
                filterExpression = b => b.SupplierId.Equals(supplierId);
            }

            // Map string orderBy to the appropriate property (use switch or reflection if necessary)
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderByFunc = q => q.OrderBy(b => b.Name); // Default ordering by Name
            if (orderBy?.ToLower() == "price")
            {
                if (IsDescending) {
                    orderByFunc = q => q.OrderByDescending(b => b.Price);
                }
                else
                {
                    orderByFunc = q => q.OrderBy(b => b.Price);
                }
            }

            var bookResponses = await bookService.GetBookAsync(
                filter: filterExpression,
                orderBy: orderByFunc,
                includeProperties: "BookImages",
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all books successfully!",
                data = bookResponses
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bookResponse = await bookService.GetBookByIdAsync(id);

            if (bookResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get book by id successfully !",
                data = bookResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO bookDTO)
        {
            var bookResponse = await bookService.AddBookAsync(bookDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create book successfully !",
                data = bookResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookDTO bookDTO)
        {
            var bookResponse = await bookService.UpdateBookAsync(id, bookDTO);
            if (bookResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update book successfully !",
                data = bookResponse
            };
            return Ok(response);
        }
    }
}
