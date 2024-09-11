using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.MessageRepo;
using Libook_API.Repositories.ParticipantRepo;

namespace Libook_API.Service.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }
        public async Task<MessageResponseDTO> AddMessageAsync(MessageDTO messageDTO)
        {
            // Map or Convert DTO to Domain Model
            var messageDomain = mapper.Map<Message>(messageDTO);

            messageDomain.SendAt = DateTime.Now;

            // Use Domain Model
            messageDomain = await messageRepository.InsertAsync(messageDomain);

            return mapper.Map<MessageResponseDTO>(messageDomain);
        }

        public async Task<IEnumerable<MessageResponseDTO?>> GetAllMessageAsync()
        {
            var messageDomains = await messageRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var messageResponse = mapper.Map<List<MessageResponseDTO>>(messageDomains);

            return messageResponse;
        }

        public async Task<MessageResponseDTO?> GetMessageByIdAsync(Guid messageId)
        {
            var messageDomain = await messageRepository.GetByIdAsync(messageId);
            return mapper.Map<MessageResponseDTO>(messageDomain);
        }

        public async Task<IEnumerable<MessageResponseDTO?>?> GetMessageByConversationIdAsync(Guid conversationId)
        {
            var messageDomains = await messageRepository.GetByConversationId(conversationId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var messageResponse = mapper.Map<List<MessageResponseDTO>>(messageDomains);

            return messageResponse;
        }

        public async Task<MessageResponseDTO?> UpdateMessageAsync(Guid messageId, MessageUpdateDTO messageUpdateDTO)
        {
            var existingMessage = await messageRepository.GetByIdAsync(messageId);
            if (existingMessage == null)
            {
                return null;
            }
            existingMessage.Content = messageUpdateDTO.Content;

            existingMessage = await messageRepository.UpdateAsync(existingMessage);

            return mapper.Map<MessageResponseDTO>(existingMessage);
        }
    }
}
