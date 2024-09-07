using Libook_API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class CommentDTO
    {
        [Required]
        [MinLength(20, ErrorMessage = "Content has to be a minimum of 20 characters")]
        public string Content { get; set; } = null!;

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }
    }

    public class CommentResponsesDTO
    {
        public Guid Id { get; set; }

        public DateTime DateCreate { get; set; }

        public string Content { get; set; } = null!;

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public Guid BookId { get; set; }

        public virtual ICollection<CommentImageResponseWithDTO> CommentImages { get; set; }
    }
}
