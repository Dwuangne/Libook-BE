using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.BookImageService;
using Libook_API.Service.CommentImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentImage : Controller
    {
        private readonly ICommentImageService commentImageService;

        public CommentImage(ICommentImageService commentImageService)
        {
            this.commentImageService = commentImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commentImageResponses = await commentImageService.GetAllCommentImageAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all comment image successfully!",
                data = commentImageResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var commentImageResponse = await commentImageService.GetCommentImageByIdAsync(id);

            if (commentImageResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get comment image by id successfully!",
                data = commentImageResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("comment/{commentId:Guid}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid commentId)
        {
            var commentImageResponse = await commentImageService.GetCommentImageByCommentIdAsync(commentId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get comment image by comment id successfully!",
                data = commentImageResponse
            };
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromBody] CommentImageDTO commentImageDTO)
        {
            var commentImageResponse = await commentImageService.AddCommentImageAsync(commentImageDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create comment image successfully!",
                data = commentImageResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CommentImageUpdateDTO commentImageUpdateDTO)
        {
            var commentImageResponse = await commentImageService.UpdateCommentImageAsync(id, commentImageUpdateDTO);
            if (commentImageResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update comment image successfully!",
                data = commentImageResponse
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var commentImageResponse = await commentImageService.DeleteCommentImageAsync(id);
            if (commentImageResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update comment image successfully!",
                data = commentImageResponse
            };
            return Ok(response);
        }
    }
}
