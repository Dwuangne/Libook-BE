using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Repositories.CommentRepo
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetByBookId(Guid bookId)
        {
            return await _dbSet.Where(comment => comment.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByUserId(Guid userId)
        {
            return await _dbSet.Where(comment => comment.UserId == userId).ToListAsync();
        }
    }
}
