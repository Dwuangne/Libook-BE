using Libook_API.Models.Domain;

namespace Libook_API.Repositories.CommentImageRepo
{
    public interface ICommentImageRepository : IGenericRepository<CommentImage>
    {
        Task<IEnumerable<CommentImage>> GetByCommentId(Guid commentId);
    }
}
