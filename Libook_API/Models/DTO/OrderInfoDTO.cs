using Libook_API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Libook_API.Models.DTO
{
    public class OrderInfoDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string ProvinceId { get; set; }

        public string DistrictId { get; set; }

        public string WardId { get; set; }

        public string Address { get; set; }

        public Guid UserId { get; set; }
    }

    public class OrderInfoUpdateDTO
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string ProvinceId { get; set; }

        public string DistrictId { get; set; }

        public string WardId { get; set; }

        public string Address { get; set; }

    }

    public class OrderInfoResponseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string ProvinceId { get; set; }

        public string DistrictId { get; set; }

        public string WardId { get; set; }

        public string Address { get; set; }

        public Guid UserId { get; set; }
    }
}
