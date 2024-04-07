using Microsoft.AspNetCore.SignalR;
using SignalR.DataService;
using SignalR.Models;

namespace SignalR.Hubs
{
    public class ChatHub: Hub
    {
        private readonly SharedDatabase _shared;
        public ChatHub(SharedDatabase shared) => _shared = shared;
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined.");
        }
        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

            _shared.connections[Context.ConnectionId] = conn;

            await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.Username} has joined {conn.ChatRoom}.");
        }
        public async Task SendMessage(string msg)
        {
            if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom)
                    .SendAsync("ReceiveSpecificMessage", conn.Username, msg);
            }
        }
    }
}
