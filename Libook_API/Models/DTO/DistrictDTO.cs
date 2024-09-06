using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class DistrictDTO
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? NameEn { get; set; }

        public string? FullName { get; set; }

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public string? ProvinceCode { get; set; }

        public int? AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

        public virtual Province? ProvinceCodeNavigation { get; set; }

        public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
    public class DistrictResponseDTO
    {
        public string Code { get; set; } = null!;
        public string? FullName { get; set; }
    }
}
