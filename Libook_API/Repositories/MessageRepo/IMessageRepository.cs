using Libook_API.Models.Domain;

namespace Libook_API.Repositories.MessageRepo
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetByConversationId(Guid conversationId);
    }
}
