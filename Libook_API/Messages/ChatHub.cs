using Azure.Identity;
using Libook_API.Data;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Service.ConversationService;
using Libook_API.Service.MessageService;
using Libook_API.Service.ParticipantService;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;

namespace Libook_API.Messages
{
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly IConversationService conversationService;
        private readonly IParticipantService participantService;
        private readonly ShareDb shareDb;

        public ChatHub(IMessageService messageService, IConversationService conversationService, IParticipantService participantService, ShareDb shareDb)
        {
            this.messageService = messageService;
            this.conversationService = conversationService;
            this.participantService = participantService;
            this.shareDb = shareDb;
        }
        public async Task SendMessage(string message)
        {
            if (shareDb.Connections.TryGetValue(Context.ConnectionId, out UserConectionDTO conn))
            {
                // Logic to save message in database can be added here

                // Broadcast message to all connected clients
                await Clients.All.SendAsync("ReceiveSepecificMessage", conn.Username, message);
            }
        }

        public async Task JoinSepecificChatRoom (UserConectionDTO conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            shareDb.Connections[Context.ConnectionId] = conn;

            await Clients.Group(conn.ChatRoom).SendAsync("JoinSepecificChatRoom", $"{conn.Username} has joined {conn.ChatRoom}");
        }
        //public override async Task OnConnectedAsync()
        //{
        //    // Thông báo khi có người tham gia
        //    await Clients.All.SendAsync("UserJoined", Context.ConnectionId);
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    // Thông báo khi có người rời khỏi chat
        //    await Clients.All.SendAsync("UserLeft", Context.ConnectionId);
        //    await base.OnDisconnectedAsync(exception);
        //}
    }
}
