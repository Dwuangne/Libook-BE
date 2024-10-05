using Libook_API.Models.DTO;
using System.Collections.Concurrent;

namespace Libook_API.Data
{
    public class ShareDb
    {
        private readonly ConcurrentDictionary<string, UserConectionDTO> _connections = new();


        public ConcurrentDictionary<string, UserConectionDTO > Connections { get { return _connections; } }
    }
}
