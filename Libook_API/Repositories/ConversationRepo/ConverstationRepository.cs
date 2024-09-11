using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Libook_API.Repositories.ConversationRepo
{
    public class ConverstationRepository : GenericRepository<Conversation>, IConverstationRepository
    {
        public ConverstationRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Conversation>> GetByUserId(Guid userId)
        {
            return await _dbSet
                .Include(c => c.Participants)  // Bao gồm những người tham gia vào cuộc hội thoại
                .Include(c => c.Messages)      // Bao gồm cả các tin nhắn trong cuộc hội thoại
                .Where(c => c.Participants.Any(p => p.UserId == userId))  // Lọc theo Participant với UserID
                .ToListAsync();
        }
    }
}
