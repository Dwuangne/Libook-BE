using Libook_API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class OrderDTO
    {

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public double Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public Guid UserId { get; set; }

        public Guid? VoucherId { get; set; }

        public Guid OrderInfoId { get; set; }

        public virtual ICollection<OrderDetailWithDTO> OrderDetails { get; set; }

        public virtual ICollection<OrderStatusWithDTO> OrderStatuses { get; set; } 
    }

    public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }

        public DateTime DateCreate { get; set; }

        public double Amount { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public Guid UserId { get; set; }

        public Guid? VoucherId { get; set; }

        public Guid OrderInfoId { get; set; }

        public virtual ICollection<OrderDetailResponseWithDTO> OrderDetails { get; set; }

        public virtual ICollection<OrderStatusResponseWithDTO> OrderStatuses { get; set; }
    }
}
