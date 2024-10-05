using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.ConversationRepo;
using Libook_API.Repositories.MessageRepo;
using Libook_API.Repositories.OrderRepo;
using Microsoft.AspNetCore.Identity;

namespace Libook_API.Service.ConversationService
{
    public class ConversationService : IConversationService
    {
        private readonly IConverstationRepository converstationRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public ConversationService(IConverstationRepository converstationRepository,UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.converstationRepository = converstationRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<ConversationResponseDTO> AddConversationAsync(ConversationDTO conversationDTO)
        {
            // Map or Convert DTO to Domain Model
            var conversationDomain = mapper.Map<Conversation>(conversationDTO);

            //Update properties 
            if (conversationDomain.Name == null)
            {
                conversationDomain.Name = "default";
            }
            conversationDomain.CreatedAt = DateTime.Now;
            foreach (var participant in conversationDomain.Participants)
            {
                participant.ConversationId = conversationDomain.Id;
                participant.JoinedAt = DateTime.Now;
            }
            foreach (var message in conversationDomain.Messages)
            {
                message.ConversationId = conversationDomain.Id;
                message.SendAt = DateTime.Now;
            }

            conversationDomain = await converstationRepository.InsertAsync(conversationDomain);

            var conversationResponse = mapper.Map<ConversationResponseDTO>(conversationDomain);
            foreach (var participant in conversationResponse.Participants)
            {
                var userInfo = await userManager.FindByIdAsync(participant.UserId.ToString());
                participant.Username = userInfo.UserName;
            }
            return conversationResponse;
        }

        public async Task<IEnumerable<ConversationResponseDTO?>> GetAllConversationAsync()
        {
            var conversationDomains = await converstationRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var conversationResponses = mapper.Map<List<ConversationResponseDTO>>(conversationDomains);

            foreach (var conversationResponse in conversationResponses)
            {
                foreach(var participant in conversationResponse.Participants)
                {
                    var userInfo = await userManager.FindByIdAsync(participant.UserId.ToString());
                    participant.Username = userInfo.UserName;
                }
            }
            return conversationResponses;
        }

        public async Task<ConversationResponseDTO?> GetConversationByIdAsync(Guid conversationId)
        {
            var conversationDomain = await converstationRepository.GetByIdAsync(conversationId);

            var conversationResponse = mapper.Map<ConversationResponseDTO>(conversationDomain);
            foreach (var participant in conversationResponse.Participants)
            {
                var userInfo = await userManager.FindByIdAsync(participant.UserId.ToString());
                participant.Username = userInfo.UserName;
            }
            return conversationResponse;
        }

        public async Task<IEnumerable<ConversationResponseDTO?>> GetConversationByUserIdAsync(Guid userId)
        {
            // Lấy danh sách các cuộc hội thoại dựa trên UserId
            var conversationDomains = await converstationRepository.GetByUserId(userId);

            // Ánh xạ các đối tượng từ domain sang DTO
            var conversationResponses = mapper.Map<List<ConversationResponseDTO>>(conversationDomains);

            // Sử dụng linq để cập nhật username cho từng participant
            foreach (var conversationResponse in conversationResponses)
            {
                foreach (var participant in conversationResponse.Participants)
                {
                    var userInfo = await userManager.FindByIdAsync(participant.UserId.ToString());
                    if (userInfo != null)
                    {
                        participant.Username = userInfo.UserName;
                    }
                }
            }

            return conversationResponses;
        }

    }
}
