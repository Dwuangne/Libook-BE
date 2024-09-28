namespace Libook_API.Models.DTO
{
    public class BookImageDTO
    {
        public string? BookImageUrl { get; set; }

        public Guid BookId { get; set; }
    }

    public class BookImageWithDTO
    {
        public string? BookImageUrl { get; set; }

    }

    public class BookImageUpdateDTO
    {
        public string? BookImageUrl { get; set; }

    }

    public class BookImageResponseDTO
    {
        public Guid Id { get; set; }

        public string? BookImageUrl { get; set; }

        public Guid BookId { get; set; }
    }

    public class BookImageResponseWithDTO
    {
        public Guid Id { get; set; }
        public string? BookImageUrl { get; set; }
    }
}
