﻿using Libook_API.Models.Domain;

namespace Libook_API.Repositories.ConversationRepo
{
    public interface IConverstationRepository : IGenericRepository<Conversation>
    {
        Task<IEnumerable<Conversation>> GetByUserId(Guid userId);
    }
}
