using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.MessageRepo
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetByConversationId(Guid conversationId)
        {
            return _dbSet.Where(message => message.ConversationId == conversationId);
        }
    }
}
