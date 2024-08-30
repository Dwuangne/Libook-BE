using Libook_API.Data;
using Libook_API.Models.Domain;

namespace Libook_API.Repositories.MessageRepo
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(LibookDbContext context) : base(context)
        {
        }
    }
}
