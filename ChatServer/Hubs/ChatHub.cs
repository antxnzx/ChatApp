using ChatServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);
    }
    public class ChatHub : Hub<IChatClient>
    {
        public async Task JoinChat(UserConnection connetion)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connetion.ChatRoom);

            await Clients
                .Group(connetion.ChatRoom)
                .ReceiveMessage("Admin", "test");
        }

        public void NotifyUsers(string login)
        {
             Console.WriteLine($"{login}");
        }
    }
}
