using Libook_API.Models.DTO;
using Libook_API.Service.BookService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Libook_API.Models.Domain;
using Libook_API.Configure;
using Libook_API.Service.OpenAIService;
using Libook_API.Utils;
using Libook_API.Models.Response;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IAzureOpenAIService azureOpenAIService;

        public OpenAIController(IBookService bookService, IAzureOpenAIService azureOpenAIService)
        {
            this.bookService = bookService;
            this.azureOpenAIService = azureOpenAIService;
        }

        [HttpGet]
        [Route("book/{bookId:Guid}")]
        public async Task<IActionResult> SugguestBookFromBookId([FromRoute] Guid bookId)
        {
            var bookOrigin = await bookService.GetBookByIdAsync(bookId);

            Expression<Func<Book, bool>> filterExpression = b => b.IsActive;
            filterExpression = filterExpression.AndAlso(b => b.Id != bookId);
            filterExpression = filterExpression.AndAlso(b => b.CategoryId == bookOrigin.CategoryId);

            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderByFunc = q => q.OrderByDescending(b => b.Price * (100 - b.PrecentDiscount) / 100);

            var bookResponses = await bookService.GetBookAsync(
                filter: filterExpression,
                orderBy: orderByFunc,
                includeProperties: "",
                pageIndex: 1,
                pageSize: 30
            );

            var potentialBooks = bookResponses.BookResponseDTOs;

            var bookRecommendations = await azureOpenAIService.GetBookRecommendationAsync(bookOrigin, potentialBooks);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get book recommendations successfully!",
                data = bookRecommendations
            };

            return Ok(response);
        }
    }
}
