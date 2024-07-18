using ChatClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels
{
    public partial class ChatViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        ObservableCollection<MessageData> messages = [];
        [ObservableProperty]
        private string usernameChat = "";
        [ObservableProperty]
        private string message = "";
        string chatroom = "";
        HubConnection? hubConnection;
        private UserConnection userConnection = new("", "");
        public ChatViewModel()
        {
            hubConnection = App.hubConnection;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("chatroom"))
            {
                chatroom = query["chatroom"].ToString();
            }
            if (query.ContainsKey("user"))
            {
                UsernameChat = query["user"].ToString();
            }
        }
        public async Task JoinChat()
        {
            userConnection = new(App.UserDetails.Login, chatroom);
            
            hubConnection.On("ReceiveMessage", (Action<string, string>)((username, text) =>
            {
                SendLocalMessage(username, text);
            }));
            await hubConnection.InvokeAsync("JoinChat", userConnection);
        }
        public async Task RemoveFromChat()
        {
            await hubConnection.InvokeAsync("RemoveFromChat", userConnection);
        }
        private void SendLocalMessage(string user, string message)
        {
            Messages.Add(new MessageData(user, message));
        }

        [RelayCommand]
        public async Task SendMessage()
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", userConnection, Message);
            }
            catch (Exception ex)
            {
                SendLocalMessage(string.Empty, $"Ошибка отправки: {ex.Message}");
            }
        }
    }

}
