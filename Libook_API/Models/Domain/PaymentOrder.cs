
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.Domain
{
    public class PaymentOrder
    {
        [Key]
        public long PaymentID { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
