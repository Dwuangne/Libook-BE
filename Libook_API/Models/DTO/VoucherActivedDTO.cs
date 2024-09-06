namespace Libook_API.Models.DTO
{
    public class VoucherActivedDTO
    {
        public Guid VoucherId { get; set; }

        public Guid UserId { get; set; }
    }

    public class VoucherActivedResponseDTO
    {
        public Guid VoucherId { get; set; }

        public Guid UserId { get; set; }
    }
}
