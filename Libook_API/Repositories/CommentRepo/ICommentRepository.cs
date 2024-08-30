using Libook_API.Models.Domain;

namespace Libook_API.Repositories.CommentRepo
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByUserId(Guid userId);
        Task<IEnumerable<Comment>> GetByBookId(Guid bookId);
    }
}
