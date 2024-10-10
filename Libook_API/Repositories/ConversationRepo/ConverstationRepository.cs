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
        public async Task<IEnumerable<Conversation>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Participants)  // Bao gồm những người tham gia vào cuộc hội thoại
                .OrderByDescending(c => c.Messages
                    .OrderByDescending(m => m.SendAt)
                    .Select(m => m.SendAt)
                    .FirstOrDefault())  // Lấy thời gian tin nhắn mới nhất để sắp xếp
                .ToListAsync();
        }
        public async Task<Conversation?> GetByIdAsync(object id)
        {
            return await _dbSet
                .Include(c => c.Participants)  // Bao gồm những người tham gia vào cuộc hội thoại    
                .FirstOrDefaultAsync(c => c.Id == (Guid)id);  // Sử dụng guidId thay vì object id
        }
        public async Task<IEnumerable<Conversation>> GetByUserId(Guid userId)
        {
            return await _dbSet
                .Include(c => c.Participants)  // Bao gồm những người tham gia vào cuộc hội thoại                                         
                .Where(c => c.Participants.Any(p => p.UserId == userId))  // Lọc theo Participant với UserID
                .ToListAsync();
        }
    }
}
