using AutoMapper;
using Libook_API.Data;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Service.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            this.authorRepository = authorRepository;
            this.mapper = mapper;
        }

        public async Task<AuthorResponseDTO> AddAuthorAsync(AuthorDTO authorDTO)
        {
            // Map or Convert DTO to Domain Model
            var authorDomain = mapper.Map<Author>(authorDTO);

            // Use Domain Model to create Author
            authorDomain = await authorRepository.InsertAsync(authorDomain);

            return mapper.Map<AuthorResponseDTO>(authorDomain);
        }

        public async Task<IEnumerable<AuthorResponseDTO>> GetAllAuthorAsync()
        {
            var authorDomains = await authorRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var authorResponse = mapper.Map<List<AuthorResponseDTO>>(authorDomains);

            return authorResponse;
        }

        public async Task<AuthorResponseDTO?> GetAuthorByIdAsync(Guid authorId)
        {
            var authorDomain = await authorRepository.GetByIdAsync(authorId);
            return mapper.Map<AuthorResponseDTO>(authorDomain);
        }

        public async Task<AuthorResponseDTO?> UpdateAuthorAsync(Guid authorId, AuthorDTO authorDTO)
        {
            var existingAuthor = await authorRepository.GetByIdAsync(authorId);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = authorDTO.Name;

            existingAuthor = await authorRepository.UpdateAsync(existingAuthor);

            return mapper.Map<AuthorResponseDTO>(existingAuthor);
        }
    }
}
