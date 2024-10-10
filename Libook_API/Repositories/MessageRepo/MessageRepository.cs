using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.MessageRepo
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetByConversationId(Guid conversationId, int pageNumber, int pageSize)
        {
            return await _dbSet
                .Where(message => message.ConversationId == conversationId)
                .OrderByDescending(message => message.SendAt)  // Lấy tin nhắn mới nhất trước
                .Skip((pageNumber - 1) * pageSize)  // Bỏ qua tin nhắn đã lấy
                .Take(pageSize)  // Lấy số lượng tin nhắn theo pageSize
                .ToListAsync();
        }

    }
}
