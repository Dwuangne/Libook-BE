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
            return await _dbSet
                .Include(b => b.CommentImages)
                .Where(comment => comment.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByUserId(Guid userId)
        {
            return await _dbSet
                .Include(b => b.CommentImages)
                .Where(comment => comment.UserId == userId).ToListAsync();
        }

        public async override Task<Comment?> GetByIdAsync(object id)
        {
            return await _context.Comments
                .Include(b => b.CommentImages) // Include the related CommentImages
                .FirstOrDefaultAsync(b => b.Id == (Guid)id); // Assuming 'id' is of type Guid
        }
    }
}
