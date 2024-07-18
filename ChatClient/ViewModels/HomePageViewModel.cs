
using ChatClient.Models;
using ChatClient.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;


namespace ChatClient.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool admin = false;
        [ObservableProperty]
        private List<UserInfo> users = [];
        public HomePageViewModel()
        {
            admin = App.UserDetails.IsAdmin == 1;
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
        async Task GoToAdminPanel()
        {
           await Shell.Current.GoToAsync($"{nameof(AdminPanelPage)}");
        }



    }
}
