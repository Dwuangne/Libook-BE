using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.ConversationService;
using Libook_API.Service.MessageService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : Controller
    {
        private readonly IConversationService conversationService;

        public ConversationController(IConversationService conversationService)
        {
            this.conversationService = conversationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conversationResponses = await conversationService.GetAllConversationAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all conversation successfully!",
                data = conversationResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var conversationResponse = await conversationService.GetConversationByIdAsync(id);

            if (conversationResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get conversation by id successfully!",
                data = conversationResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("user/{userId:guid}")]
        public async Task<IActionResult> GetByUsername([FromRoute] Guid userId)
        {
            var conversationResponse = await conversationService.GetConversationByUserIdAsync(userId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get conversation by user id successfully!",
                data = conversationResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConversationDTO conversationDTO)
        {
            var conversationResponse = await conversationService.AddConversationAsync(conversationDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create conversation successfully!",
                data = conversationResponse
            };
            return Ok(response);
        }
    }
}
