using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class OrderDTO
    {
        public double Amount { get; set; }

        public string PaymentMethod { get; set; }

        public Guid UserId { get; set; }

        public Guid? VoucherId { get; set; }

        public Guid OrderInfoId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderResponseDTO
    {
        public Guid Id { get; set; }

        public DateTime DateCreate { get; set; }

        public double Amount { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public Guid UserId { get; set; }

        public Guid? VoucherId { get; set; }

        public Guid OrderInfoId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }
    }
}
