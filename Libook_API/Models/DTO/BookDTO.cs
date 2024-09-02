using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class BookDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public float PrecentDiscount { get; set; }

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

        public Guid AuthorId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SupplierId { get; set; }

        //public virtual ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();
    }
}
