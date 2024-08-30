using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.ConversationRepo
{
    public class ConverstationRepository : GenericRepository<Conversation>, IConverstationRepository
    {
        public ConverstationRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
