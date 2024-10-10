using Azure.Identity;
using Libook_API.Data;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Service.ConversationService;
using Libook_API.Service.MessageService;
using Libook_API.Service.ParticipantService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;

namespace Libook_API.Messages
{
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly IConversationService conversationService;
        private readonly IParticipantService participantService;
        private readonly UserManager<IdentityUser> userManager;

        public ChatHub(IMessageService messageService, IConversationService conversationService, IParticipantService participantService, UserManager<IdentityUser> userManager)
        {
            this.messageService = messageService;
            this.conversationService = conversationService;
            this.participantService = participantService;
            this.userManager = userManager;
        }

        public async Task JoinConversation(Guid conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public async Task SendMessage(Guid conversationId, string content, Guid userId)
        {
            var conversationResponse = await conversationService.GetConversationByIdAsync(conversationId);

            if (conversationResponse != null)
            {
                var isParticipant = conversationResponse.Participants.Any(p => p.UserId == userId);

                if (!isParticipant)
                {
                    var participantDTO = new ParticipantDTO { UserId = userId, ConversationId = conversationId };
                    await participantService.AddParticipantAsync(participantDTO);
                    await Groups.AddToGroupAsync(Context.ConnectionId, conversationResponse.Id.ToString());
                    await Clients.Group(conversationResponse.Id.ToString()).SendAsync("JoinSpecificConversation", userId);
                }

                var messageDTO = new MessageDTO
                {
                    Content = content,
                    ConversationId = conversationId,
                    UserId = userId,
                };
                var messageResponse = await messageService.AddMessageAsync(messageDTO);

                await Clients.Group(conversationResponse.Id.ToString()).SendAsync("ReceiveSpecificMessage", messageResponse);
            }
            else
            {
                throw new Exception("Conversation not found");
            }
        }

        public async Task StartConversation(Guid userId, string content)
        {
            var userInfo = await userManager.FindByIdAsync(userId.ToString());

            if (userInfo == null)
            {
                throw new Exception("User not found");
            }

            var messageWithDTO = new MessageWithDTO { Content = content, UserId = userId };
            var participantWithDTO = new ParticipantWithDTO { UserId = userId };
            var conversationDTO = new ConversationDTO
            {
                Messages = new List<MessageWithDTO> { messageWithDTO },
                Participants = new List<ParticipantWithDTO> { participantWithDTO }
            };

            var conversationResponse = await conversationService.AddConversationAsync(conversationDTO);

            if (conversationResponse == null)
            {
                throw new Exception("Failed to create conversation");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, conversationResponse.Id.ToString());

            await Clients.Group(conversationResponse.Id.ToString()).SendAsync("NotifyConversationCreatedToCustomer", conversationResponse);

            await Clients.All.SendAsync("NotifyConversationCreatedToAdmin", conversationResponse);
        }
    }
}
