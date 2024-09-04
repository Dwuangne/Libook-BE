using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.CommentRepo;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace Libook_API.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<CommentResponsesDTO> AddCommentAsync(CommentDTO commentDTO)
        {
            // Map or Convert DTO to Domain Model
            var commentDomain = mapper.Map<Comment>(commentDTO);

            commentDomain.DateCreate = DateTime.Now;
            commentDomain.UserId = commentDTO.UserId;

            // Use Domain Model to create 
            commentDomain = await commentRepository.InsertAsync(commentDomain);

            var commentResponse = mapper.Map<CommentResponsesDTO>(commentDomain);

            var userInfo = await userManager.FindByIdAsync(commentResponse.UserId.ToString());

            commentResponse.Username = userInfo.UserName;

            return commentResponse;
        }

        public async Task<IEnumerable<CommentResponsesDTO?>> GetCommentByBookIdAsync(Guid bookId)
        {
            var commentDomains = await commentRepository.GetByBookId(bookId);

            var commentResponse = mapper.Map<List<CommentResponsesDTO>>(commentDomains);
            foreach (var comment in commentResponse)
            {
                var userInfo = await userManager.FindByIdAsync(comment.UserId.ToString());

                comment.Username = userInfo.UserName;
            }
            return commentResponse;
        }

        public async Task<CommentResponsesDTO?> GetCommentByIdAsync(Guid commentId)
        {
            var commentDomain = await commentRepository.GetByIdAsync(commentId);
            var commentResponse = mapper.Map<CommentResponsesDTO>(commentDomain);

            var userInfo = await userManager.FindByIdAsync(commentResponse.UserId.ToString());
            commentResponse.Username = userInfo.UserName;

            return commentResponse;
        }

        public async Task<IEnumerable<CommentResponsesDTO?>> GetCommentByUserIdAsync(Guid userId)
        {
            var commentDomains = await commentRepository.GetByUserId(userId);

            var commentResponse = mapper.Map<List<CommentResponsesDTO>>(commentDomains);
            foreach (var comment in commentResponse)
            {
                var userInfo = await userManager.FindByIdAsync(comment.UserId.ToString());

                comment.Username = userInfo.UserName;
            }
            return commentResponse;
        }
    }
}
