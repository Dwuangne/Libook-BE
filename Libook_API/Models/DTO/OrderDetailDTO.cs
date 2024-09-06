using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class OrderDetailDTO
    {
        public Guid Id { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

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
