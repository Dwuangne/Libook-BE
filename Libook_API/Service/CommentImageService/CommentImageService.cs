using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.BookImageRepo;
using Libook_API.Repositories.CommentImageRepo;
using System.Net;

namespace Libook_API.Service.CommentImageService
{
    public class CommentImageService : ICommentImageService
    {
        private readonly ICommentImageRepository commentImageRepository;
        private readonly IMapper mapper;

        public CommentImageService(ICommentImageRepository commentImageRepository, IMapper mapper)
        {
            this.commentImageRepository = commentImageRepository;
            this.mapper = mapper;
        }

        public async Task<CommentImageResponseDTO> AddCommentImageAsync(CommentImageDTO commentImageDTO)
        {
            // Map or Convert DTO to Domain Model
            var commentImageDomain = mapper.Map<CommentImage>(commentImageDTO);

            // Use Domain Model
            commentImageDomain = await commentImageRepository.InsertAsync(commentImageDomain);

            return mapper.Map<CommentImageResponseDTO>(commentImageDomain);
        }

        public async Task<CommentImageResponseDTO?> DeleteCommentImageAsync(Guid commentImageId)
        {
            var existingCommentImage = await commentImageRepository.GetByIdAsync(commentImageId);
            if (existingCommentImage == null)
            {
                return null;
            }
            var commentImageDomain = await commentImageRepository.DeleteAsync(commentImageId);

            return mapper.Map<CommentImageResponseDTO?>(commentImageDomain);
        }

        public async Task<IEnumerable<CommentImageResponseDTO?>> GetAllCommentImageAsync()
        {
            var commentImageDomain = await commentImageRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var commmentResponse = mapper.Map<List<CommentImageResponseDTO>>(commentImageDomain);

            return commmentResponse;
        }

        public async Task<IEnumerable<CommentImageResponseDTO>?> GetCommentImageByCommentIdAsync(Guid commentId)
        {
            var commentImageDomain = await commentImageRepository.GetByCommentId(commentId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var commentResponse = mapper.Map<List<CommentImageResponseDTO>>(commentImageDomain);

            return commentResponse;
        }

        public async Task<CommentImageResponseDTO?> GetCommentImageByIdAsync(Guid commentImageId)
        {
            var commentImageDomain = await commentImageRepository.GetByIdAsync(commentImageId);
            return mapper.Map<CommentImageResponseDTO>(commentImageDomain);
        }

        public async Task<CommentImageResponseDTO?> UpdateCommentImageAsync(Guid commentImageId, CommentImageUpdateDTO commentImageUpdateDTO)
        {
            var existingCommentImage = await commentImageRepository.GetByIdAsync(commentImageId);
            if (existingCommentImage == null)
            {
                return null;
            }

            existingCommentImage.CommentImageUrl = commentImageUpdateDTO.CommentImageUrl;

            existingCommentImage = await commentImageRepository.UpdateAsync(existingCommentImage);

            return mapper.Map<CommentImageResponseDTO>(existingCommentImage);
        }
    }
}
