namespace Libook_API.Models.DTO
{
    public class BookListDTO
    {
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public List<BookResponseDTO> BookResponseDTOs { get; set; }
    }
}
