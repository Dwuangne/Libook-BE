using Libook_API.Models.DTO;

namespace Libook_API.Service.AuthorService
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDTO?>> GetAllAuthorAsync();
        Task<AuthorResponseDTO?> GetAuthorByIdAsync(Guid authorId);
        Task<AuthorResponseDTO> AddAuthorAsync(AuthorDTO authorDTO);
        Task<AuthorResponseDTO?> UpdateAuthorAsync(Guid authorId, AuthorDTO authorDTO);
    }
}
