using Libook_API.Models.Domain;
using Libook_API.Service.BookService;
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class OrderDetailDTO
    {
        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public Guid BookId { get; set; }
    }

    public class OrderDetailWithDTO
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public double UnitPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        public Guid BookId { get; set; }
    }


    public class OrderDetailResponseDTO
    {
        public Guid Id { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public Guid BookId { get; set; }

    }
    public class OrderDetailResponseWithDTO
    {
        public Guid Id { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Guid BookId { get; set; }
    }
}
