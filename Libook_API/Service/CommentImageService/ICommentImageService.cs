using Libook_API.Models.DTO;

namespace Libook_API.Service.CommentImageService
{
    public interface ICommentImageService
    {
        Task<IEnumerable<CommentImageResponseDTO?>> GetAllCommentImageAsync();
        Task<CommentImageResponseDTO?> GetCommentImageByIdAsync(Guid commentImageId);
        Task<IEnumerable<CommentImageResponseDTO>?> GetCommentImageByCommentIdAsync(Guid commentId);
        Task<CommentImageResponseDTO> AddCommentImageAsync(CommentImageDTO commentImageDTO);
        Task<CommentImageResponseDTO?> UpdateCommentImageAsync(Guid commentImageId, CommentImageUpdateDTO commentImageUpdateDTO);
        Task<CommentImageResponseDTO?> DeleteCommentImageAsync(Guid commentImageId);
    }
}
