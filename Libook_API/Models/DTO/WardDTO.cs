using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class WardDTO
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? NameEn { get; set; }

        public string? FullName { get; set; }

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public string? DistrictCode { get; set; }

        public int? AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

        public virtual District? DistrictCodeNavigation { get; set; }
    }

    public class WardResponseDTO
    {
        public string Code { get; set; } = null!;

        public string? FullName { get; set; }
    }
}
