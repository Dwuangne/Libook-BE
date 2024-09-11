using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.AuthorService;
using Libook_API.Service.ParticipantService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IParticipantService participantService;

        public ParticipantController(IParticipantService participantService)
        {
            this.participantService = participantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var participantResponses = await participantService.GetAllParticipantAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all participant successfully!",
                data = participantResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var participantResponse = await participantService.GetParticipantByIdAsync(id);

            if (participantResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get participant by id successfully!",
                data = participantResponse
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("conversation/{conversationId:Guid}")]
        public async Task<IActionResult> GetByConversationId([FromRoute] Guid conversationId)
        {
            var participantResponse = await participantService.GetParticipantByConversationIdAsync(conversationId);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get participant by conversation id successfully!",
                data = participantResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParticipantDTO participantDTO)
        {
            var participantResponse = await participantService.AddParticipantAsync(participantDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create participant successfully!",
                data = participantResponse
            };
            return Ok(response);
        }
    }
}
