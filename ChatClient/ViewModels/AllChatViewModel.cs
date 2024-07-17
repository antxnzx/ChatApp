using ChatClient.Models;
using ChatClient.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;


namespace ChatClient.ViewModels
{
    public partial class AllChatsViewModel : ObservableObject
    {


        [ObservableProperty]
        private List<UserInfo> users = new();



        public AllChatsViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public async Task GetAllUsers()
        {
            Uri uri = new Uri(string.Format($"http://localhost:5000/users", string.Empty));
            HttpClient client = App.httpClient;
            if (client != null)
            {
                List<UserInfo>? usersfromdb = await client.GetFromJsonAsync<List<UserInfo>>(uri);
                if (usersfromdb != null)
                {
                    Users.AddRange(usersfromdb);
                }
            }
        }

        [RelayCommand]
        public async Task StartChat(UserInfo user)
        {
            if (user != null)
            {
                int id = App.UserDetails.id;
                string chatroom = user.Id > id ? $"{id}{user.Id}" : $"{user.Id}{id}";
                
                try
                {
                    
                    await Shell.Current.GoToAsync($"{nameof(ChatView)}?chatroom={chatroom}");

                }
                catch (Exception)
                {


                }
            }
        }
    }
}
