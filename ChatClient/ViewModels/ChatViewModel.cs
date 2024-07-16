using ChatClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;


namespace ChatClient.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private HubConnection ?hubConnection;

        public string UserName { get; set; }
        public string Message { get; set; }

        [ObservableProperty]
        private List<MessageData> messages = new();

        [ObservableProperty]
        private bool isBusy;
        [ObservableProperty]
        private bool isConnected;

        public ChatViewModel()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5267/chat")
                .WithAutomaticReconnect()
                .Build();
        }
    }
}
