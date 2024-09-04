using Libook_API.Models.DTO;

namespace Libook_API.Service.CommentService
{
    public interface ICommentService
    {
        Task<CommentResponsesDTO?> GetCommentByIdAsync(Guid commentId);

        Task<IEnumerable<CommentResponsesDTO?>> GetCommentByBookIdAsync(Guid bookId);
        Task<IEnumerable<CommentResponsesDTO?>> GetCommentByUserIdAsync(Guid userId);
        Task<CommentResponsesDTO> AddCommentAsync(CommentDTO commentDTO);
    }
}
