using AutoMapper;
using Libook_API.Configure;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.BookService;
using Microsoft.AspNetCore.Authorization;
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
             [FromQuery] string? filter = null,
             [FromQuery] string? authorId = null,
             [FromQuery] string? categoryId = null,
             [FromQuery] string? supplierId = null,
             [FromQuery] string? orderBy = "name",
             [FromQuery] bool IsDescending = false,
             [FromQuery] int pageIndex = 1,
             [FromQuery] int pageSize = 12)
        {
            // Start with a base filter
            Expression<Func<Book, bool>> filterExpression = b => b.IsActive;

            // Combine filters using AndAlso method
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = filterExpression.AndAlso(b => b.Name.Contains(filter));
            }

            if (!string.IsNullOrEmpty(authorId))
            {
                var authorGuid = Guid.Parse(authorId);
                filterExpression = filterExpression.AndAlso(b => b.AuthorId == authorGuid);
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                var categoryGuid = Guid.Parse(categoryId);
                filterExpression = filterExpression.AndAlso(b => b.CategoryId == categoryGuid);
            }

            if (!string.IsNullOrEmpty(supplierId))
            {
                var supplierGuid = Guid.Parse(supplierId);
                filterExpression = filterExpression.AndAlso(b => b.SupplierId == supplierGuid);
            }

            // Map string orderBy to the appropriate property (use switch or reflection if necessary)
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderByFunc = null; // Default ordering by Name
            if (orderBy?.ToLower() == "price")
            {
                orderByFunc = IsDescending ? q => q.OrderByDescending(b => b.Price * (100 - b.PrecentDiscount) / 100)
                                           : q => q.OrderBy(b => b.Price * (100 - b.PrecentDiscount) / 100);
            }
            if (orderBy?.ToLower() == "name")
            {
                orderByFunc = IsDescending ? q => q.OrderByDescending(b => b.Name) : q => q.OrderBy(b => b.Name);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            var bookResponse = await bookService.UpdateBookAsync(id, bookUpdateDTO);
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
