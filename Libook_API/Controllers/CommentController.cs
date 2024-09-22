using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.CommentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var commentResponse = await commentService.GetCommentByIdAsync(id);

            if (commentResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get comment by id successfully!",
                data = commentResponse
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("book/{bookId:Guid}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId)
        {
            var commentResponses = await commentService.GetCommentByBookIdAsync(bookId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get comment by book id successfully!",
                data = commentResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("user/{userId:Guid}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            var commentResponses = await commentService.GetCommentByUserIdAsync(userId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get comment by user id successfully!",
                data = commentResponses
            };
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Create([FromBody] CommentDTO commentDTO)
        {
            var commentResponse = await commentService.AddCommentAsync(commentDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create author successfully!",
                data = commentResponse
            };
            return Ok(response);
        }
    }
}
