using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.ParticipantRepo
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
