using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class AuthorDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class AuthorResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
