using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;

namespace Libook_API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<Author, AuthorResponseDTO>().ReverseMap();

            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryResponseDTO>().ReverseMap();

            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<Supplier, SupplierResponseDTO>().ReverseMap();

            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<Book, BookResponseDTO>().ReverseMap();
        }
    }
}
