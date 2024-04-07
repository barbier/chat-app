using SignalR.Models;
using System.Collections.Concurrent;

namespace SignalR.DataService
{
    public class SharedDatabase
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection = new ConcurrentDictionary<string, UserConnection>();
        public ConcurrentDictionary<string, UserConnection> connections => _connection;
    }
}
