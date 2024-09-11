using Libook_API.Models.DTO;

namespace Libook_API.Service.MessageService
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResponseDTO?>> GetAllMessageAsync();
        Task<MessageResponseDTO?> GetMessageByIdAsync(Guid messageId);
        Task<IEnumerable<MessageResponseDTO?>?> GetMessageByConversationIdAsync(Guid conversationId);
        Task<MessageResponseDTO> AddMessageAsync(MessageDTO messageDTO);
        Task<MessageResponseDTO?> UpdateMessageAsync(Guid messageId, MessageUpdateDTO messageUpdateDTO);
    }
}
