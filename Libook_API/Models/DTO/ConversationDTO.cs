using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class ConversationDTO
    {
        public string? Name { get; set; }

        public virtual ICollection<MessageWithDTO> Messages { get; set; } 

        public virtual ICollection<ParticipantWithDTO> Participants { get; set; }
    }
    public class ConversationResponseDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<MessageResponseWithDTO> Messages { get; set; } 

        public virtual ICollection<ParticipantResponseWithDTO> Participants { get; set; } 
    }
}
