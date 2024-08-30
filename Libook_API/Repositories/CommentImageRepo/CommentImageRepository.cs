using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.CommentImageRepo
{
    public class CommentImageRepository : GenericRepository<CommentImage>, ICommentImageRepository
    {
        public CommentImageRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
