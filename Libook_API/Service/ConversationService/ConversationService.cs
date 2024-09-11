using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.ConversationRepo;
using Libook_API.Repositories.MessageRepo;
using Libook_API.Repositories.OrderRepo;

namespace Libook_API.Service.ConversationService
{
    public class ConversationService : IConversationService
    {
        private readonly IConverstationRepository converstationRepository;
        private readonly IMapper mapper;

        public ConversationService(IConverstationRepository converstationRepository, IMapper mapper)
        {
            this.converstationRepository = converstationRepository;
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

            return mapper.Map<ConversationResponseDTO>(conversationDomain);
        }

        public async Task<IEnumerable<ConversationResponseDTO?>> GetAllConversationAsync()
        {
            var conversationDomains = await converstationRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var conversationResponse = mapper.Map<List<ConversationResponseDTO>>(conversationDomains);

            return conversationResponse;
        }

        public async Task<ConversationResponseDTO?> GetConversationByIdAsync(Guid conversationId)
        {
            var conversationDomain = await converstationRepository.GetByIdAsync(conversationId);
            return mapper.Map<ConversationResponseDTO>(conversationDomain);
        }

        public async Task<IEnumerable<ConversationResponseDTO?>?> GetConversationByUserIdAsync(Guid userId)
        {
            var conversationDomains = await converstationRepository.GetByUserId(userId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var conversationResponse = mapper.Map<List<ConversationResponseDTO>>(conversationDomains);

            return conversationResponse;
        }
    }
}
