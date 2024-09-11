using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class ParticipantDTO
    {
        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }
    }
    public class ParticipantWithDTO
    {
        public Guid UserId { get; set; }
    }
    public class ParticipantResponseDTO
    {
        public Guid Id { get; set; }

        public DateTime JoinedAt { get; set; }

        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }
    }
    public class ParticipantResponseWithDTO
    {
        public Guid Id { get; set; }

        public DateTime JoinedAt { get; set; }

        public Guid UserId { get; set; }
    }
}
