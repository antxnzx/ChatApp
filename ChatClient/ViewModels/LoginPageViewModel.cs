using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ChatClient.Models;
using ChatClient.Views;
using ChatClient.ViewModels;


namespace ChatClient.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string ?emailLog;
        [ObservableProperty]
        private string ?password;

        private bool isUserDataAccepted;

        HttpClient client;

        public LoginPageViewModel()
        {
            client = new HttpClient();
 
        }

        [RelayCommand]
        async Task Login() {
            Uri uri = new Uri(string.Format($"http://localhost:5000/login?login={emailLog}&password={password}", string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response!= null)
            {
                Console.WriteLine(response);
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

    }
}
