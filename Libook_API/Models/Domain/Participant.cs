using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Participant
{
    public Participant()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public DateTime JoinedAt { get; set; }

    public Guid ConversationId { get; set; }

    public Guid UserId { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;
}
