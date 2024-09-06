using Libook_API.Models.Domain;

namespace Libook_API.Models.DTO
{
    public class ProvinceDTO
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? NameEn { get; set; }

        public string FullName { get; set; } = null!;

        public string? FullNameEn { get; set; }

        public string? CodeName { get; set; }

        public int? AdministrativeUnitId { get; set; }

        public int? AdministrativeRegionId { get; set; }

        public virtual AdministrativeRegion? AdministrativeRegion { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }

        public virtual ICollection<District> Districts { get; set; } = new List<District>();
    }

    public class ProvinceResponseDTO
    {
        public string Code { get; set; } = null!;

        public string FullName { get; set; } = null!;
    }
}
