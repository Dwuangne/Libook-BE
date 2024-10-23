using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class ArticleDTO
    {
        public required string Title { get; set; }

        public string? Summary { get; set; }

        public required string UrlPicture { get; set; }

        public required string Link { get; set; }
    }
}
