using Libook_API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class BookDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Precent discount must be between 1 and 100")]
        public float PrecentDiscount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Remain must be greater than 0")]
        public int Remain { get; set; }

        public bool IsActive { get; set; }

        public Guid AuthorId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SupplierId { get; set; }

        [Required]
        public virtual ICollection<BookImageWithDTO> BookImages { get; set; }
    }

    public class BookUpdateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Precent discount must be between 1 and 100")]
        public float PrecentDiscount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Remain must be greater than 0")]
        public int Remain { get; set; }

        public bool IsActive { get; set; }

        public Guid AuthorId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SupplierId { get; set; }
    }

    public class BookResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public float PrecentDiscount { get; set; }

        public int Remain { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public Guid AuthorId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SupplierId { get; set; }

        public virtual ICollection<BookImageResponseWithDTO> BookImages { get; set; }
    }
}
