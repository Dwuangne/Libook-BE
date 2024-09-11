using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class MessageDTO
    {
        public string Content { get; set; } = null!;

        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }
    }

    public class MessageWithDTO
    {
        public string Content { get; set; } = null!;

        public Guid UserId { get; set; }
    }

    public class MessageUpdateDTO
    {
        public string Content { get; set; } = null!;

    }

    public class MessageResponseDTO
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime SendAt { get; set; }

        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }

    }

    public class MessageResponseWithDTO
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime SendAt { get; set; }

        public Guid UserId { get; set; }

    }
}
