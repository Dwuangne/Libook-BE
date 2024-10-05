using Libook_API.Models.DTO;

namespace Libook_API.Service.ConversationService
{
    public interface IConversationService
    {
        Task<IEnumerable<ConversationResponseDTO?>> GetAllConversationAsync();
        Task<ConversationResponseDTO?> GetConversationByIdAsync(Guid conversationId);
        Task<IEnumerable<ConversationResponseDTO?>> GetConversationByUserIdAsync(Guid userId);
        Task<ConversationResponseDTO> AddConversationAsync(ConversationDTO conversationDTO);
    }
}
