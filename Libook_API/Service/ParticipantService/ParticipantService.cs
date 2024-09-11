using AutoMapper;
using Libook_API.Models.Domain;
using Libook_API.Models.DTO;
using Libook_API.Repositories.AuthorRepo;
using Libook_API.Repositories.ParticipantRepo;

namespace Libook_API.Service.ParticipantService
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository participantRepository;
        private readonly IMapper mapper;

        public ParticipantService(IParticipantRepository participantRepository, IMapper mapper)
        {
            this.participantRepository = participantRepository;
            this.mapper = mapper;
        }
        public async Task<ParticipantResponseDTO> AddParticipantAsync(ParticipantDTO participantDTO)
        {
            // Map or Convert DTO to Domain Model
            var participantDomain = mapper.Map<Participant>(participantDTO);

            participantDomain.JoinedAt = DateTime.Now;

            // Use Domain Model
            participantDomain = await participantRepository.InsertAsync(participantDomain);

            return mapper.Map<ParticipantResponseDTO>(participantDomain);
        }

        public async Task<IEnumerable<ParticipantResponseDTO?>> GetAllParticipantAsync()
        {
            var participantDomains = await participantRepository.GetAllAsync();

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var participantResponse = mapper.Map<List<ParticipantResponseDTO>>(participantDomains);

            return participantResponse;
        }

        public async Task<IEnumerable<ParticipantResponseDTO?>?> GetParticipantByConversationIdAsync(Guid conversationId)
        {
            var participantDomains = await participantRepository.GetByConversationId(conversationId);

            // Giả sử authorDomains là danh sách các đối tượng AuthorDomain
            var participantResponse = mapper.Map<List<ParticipantResponseDTO>>(participantDomains);

            return participantResponse;
        }

        public async Task<ParticipantResponseDTO?> GetParticipantByIdAsync(Guid participantId)
        {
            var participantDomain = await participantRepository.GetByIdAsync(participantId);
            return mapper.Map<ParticipantResponseDTO>(participantDomain);
        }
    }
}
