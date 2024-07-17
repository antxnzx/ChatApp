using ChatClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.ViewModels
{
    public partial class ChatViewModel : BaseViewModel, IQueryAttributable
    {
        public ChatViewModel()
        {
            
        }
        [ObservableProperty]
        List<MessageData> messages = [];
        string chatroom = "";
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("chatroom"))
            {
                chatroom = query["chatroom"].ToString();
            }

        }
        public async Task JoinChat()
        {
            UserConnection userConnection = new(App.UserDetails.Login, chatroom);
            App.hubConnection.On<string, string>("ReceiveMessage", (username, text) =>
            {
                SendLocalMessage(username, text);
            });
            await App.hubConnection.InvokeAsync("JoinChat", userConnection);
        }
        private void SendLocalMessage(string user, string message)
        {
            Messages.Add(new MessageData(user, message));
        }
    }

}
