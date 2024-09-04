using Libook_API.Data;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Libook_API.Repositories.CommentImageRepo
{
    public class CommentImageRepository : GenericRepository<CommentImage>, ICommentImageRepository
    {
        public CommentImageRepository(LibookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CommentImage>> GetByCommentId(Guid commentId)
        {
            return await _dbSet.Where(commentImage => commentImage.CommentId == commentId).ToListAsync();
        }
    }
}
