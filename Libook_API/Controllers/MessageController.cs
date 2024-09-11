using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.MessageService;
using Libook_API.Service.ParticipantService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messageResponses = await messageService.GetAllMessageAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all message successfully!",
                data = messageResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var messageResponse = await messageService.GetMessageByIdAsync(id);

            if (messageResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get message by id successfully!",
                data = messageResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("conversation/{conversationId:Guid}")]
        public async Task<IActionResult> GetByConversationId([FromRoute] Guid conversationId)
        {
            var messageResponse = await messageService.GetMessageByConversationIdAsync(conversationId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get message by conversation id successfully!",
                data = messageResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MessageDTO messageDTO)
        {
            var messageResponse = await messageService.AddMessageAsync(messageDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create message successfully!",
                data = messageResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MessageUpdateDTO messageUpdateDTO)
        {
            var messageResponse = await messageService.UpdateMessageAsync(id, messageUpdateDTO);

            if (messageResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update message successfully!",
                data = messageResponse
            };
            return Ok(response);
        }
    }
}
