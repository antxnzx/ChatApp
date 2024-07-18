using ChatServer.Models;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ChatServer.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);
    }
    public class ChatHub : Hub<IChatClient>
    {

        public ChatHub()
        {


        }
        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                     .Group(connection.ChatRoom)
                     .ReceiveMessage(connection.UserName, "присоединился к чату");

        }
        public async Task SendMessage(UserConnection connection, string message)
        {

            if (connection != null)
            {
                await Clients
                    .Group(connection.ChatRoom)
                    .ReceiveMessage(connection.UserName, message);

            }

        }
        public async Task RemoveFromChat(UserConnection connection)
        {

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);
            await Clients
                     .Group(connection.ChatRoom)
                     .ReceiveMessage(connection.UserName, "вышел из чата");
        }
        public void NotifyUsers(string login)
        {
            Console.WriteLine($"{login}");
        }
    }
}
