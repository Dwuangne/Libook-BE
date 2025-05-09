﻿using Libook_API.Models.Domain;

namespace Libook_API.Repositories.ParticipantRepo
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByConversationId(Guid conversationId);
    }
}
