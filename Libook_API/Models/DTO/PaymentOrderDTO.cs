namespace Libook_API.Models.DTO
{
    public class PaymentOrderDTO
    {
        public PaymentOrderDTO(decimal amount, Guid orderId)
        {
            Amount = amount;
            OrderId = orderId;
        }

        public decimal Amount { get; set; }

        public Guid OrderId { get; set; }
    }

    public class PaymentOrderResponseDTO
    {
        public long PaymentID { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid OrderId { get; set; }
    }
}
