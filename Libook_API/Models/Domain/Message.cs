using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Message
{
    public Message()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SendAt { get; set; }

    public Guid ConversationId { get; set; }

    public Guid UserId { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;
}
