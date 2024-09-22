using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.BookImageService;
using Libook_API.Service.BookService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookImageController : Controller
    {
        private readonly IBookImageService bookImageService;

        public BookImageController(IBookImageService bookImageService)
        {
            this.bookImageService = bookImageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookImageResponses = await bookImageService.GetAllBookImageAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all book image successfully!",
                data = bookImageResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bookImageResponse = await bookImageService.GetBookImageByIdAsync(id);

            if (bookImageResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get book image by id successfully!",
                data = bookImageResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("book/{bookId:Guid}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId)
        {
            var bookImageResponse = await bookImageService.GetBookImageByBookIdAsync(bookId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get book image by book id successfully!",
                data = bookImageResponse
            };
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BookImageDTO bookImageDTO)
        {
            var bookImageResponse = await bookImageService.AddBookImageAsync(bookImageDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create book image successfully!",
                data = bookImageResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookImageUpdateDTO bookImageUpdateDTO)
        {
            var bookImageResponse = await bookImageService.UpdateBookImageAsync(id, bookImageUpdateDTO);
            if (bookImageResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update book image successfully!",
                data = bookImageResponse
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var bookImageResponse = await bookImageService.DeleteBookImageAsync(id);
            if (bookImageResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Delete book image successfully!",
                data = bookImageResponse
            };
            return Ok(response);
        }
    }
}
