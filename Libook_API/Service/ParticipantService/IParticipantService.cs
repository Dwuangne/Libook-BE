using Libook_API.Models.DTO;

namespace Libook_API.Service.ParticipantService
{
    public interface IParticipantService
    {
        Task<IEnumerable<ParticipantResponseDTO?>> GetAllParticipantAsync();
        Task<ParticipantResponseDTO?> GetParticipantByIdAsync(Guid participantId);
        Task<IEnumerable<ParticipantResponseDTO?>?> GetParticipantByConversationIdAsync(Guid conversationId);
        Task<ParticipantResponseDTO> AddParticipantAsync(ParticipantDTO participantDTO);
    }
}
