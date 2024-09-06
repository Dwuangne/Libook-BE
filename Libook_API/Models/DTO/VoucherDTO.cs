namespace Libook_API.Models.DTO
{
    public class VoucherDTO
    {
        public string Title { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Discount { get; set; }

        public int Remain { get; set; }
    }

    public class VoucherResponseDTO
    {
        public Guid VoucherId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Discount { get; set; }

        public int Remain { get; set; }
    }
    public class VoucherRemainUpdateDTO
    {
        public int Remain { get; set; }
    }
}
