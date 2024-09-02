namespace Libook_API.Models.DTO
{
    public class CategoryDTO
    {
        public string Name { get; set; }
    }

    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
