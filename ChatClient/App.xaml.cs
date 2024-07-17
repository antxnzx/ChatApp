using ChatClient.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient
{
    public partial class App : Application
    {
        public static UserBasicInfo? UserDetails;
        public static HubConnection? hubConnection;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
