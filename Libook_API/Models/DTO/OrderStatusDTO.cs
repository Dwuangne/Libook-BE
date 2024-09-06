namespace Libook_API.Models.DTO
{
    public class OrderStatusDTO
    {
        public string Status { get; set; } = null!; //unpaid pending preparing delivering .....

        public Guid OrderId { get; set; }
    }
    public class OrderStatusResponseDTO
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = null!; //unpaid pending preparing delivering .....

        public DateTime DateCreate { get; set; }

        public Guid OrderId { get; set; }
    }
    public class OrderStatusResponseWithDTO
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = null!; //unpaid pending preparing delivering .....

        public DateTime DateCreate { get; set; }
    }
}
