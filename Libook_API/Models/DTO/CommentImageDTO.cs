namespace Libook_API.Models.DTO
{
    public class CommentImageDTO
    {
        public string? CommentImageUrl { get; set; }

        public Guid CommentId { get; set; }
    }

    public class CommentImageUpdateDTO
    {
        public string? CommentImageUrl { get; set; }

    }

    public class CommentImageResponseDTO
    {
        public Guid Id { get; set; }
        public string? CommentImageUrl { get; set; }

        public Guid CommentId { get; set; }
    }

    public class CommentImageResponseWithDTO
    {
        public Guid Id { get; set; }
        public string? CommentImageUrl { get; set; }
    }
}
