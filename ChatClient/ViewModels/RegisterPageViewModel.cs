using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ChatClient.Models;
using ChatClient.Views;
using ChatClient.ViewModels;
using System.Net.Http.Json;


namespace ChatClient.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? emailLog;
        [ObservableProperty]
        private string? password;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private string? surname;
        [ObservableProperty]
        private string? logininfo = "";

        private bool isUserDataAccepted = false;

        HttpClient client;

        public RegisterPageViewModel()
        {
            client = new HttpClient();

        }

        [RelayCommand]
        async Task Register()
        {
            if (!String.IsNullOrWhiteSpace(EmailLog)
                && !String.IsNullOrWhiteSpace(Password)
                && !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Surname))
            {

                int dbid = 1;
                Uri uri = new(string.Format($"http://localhost:5000/register/{dbid}", string.Empty));

                UserInfo info = new(EmailLog, Name, Surname);
                UserLoginInfo info2 = new(EmailLog, Password);

                HttpResponseMessage response2 = await client.PostAsJsonAsync(uri, info2);
                bool? check2 = await response2.Content.ReadFromJsonAsync<bool>();
                dbid++;
                uri = new(string.Format($"http://localhost:5000/register/{dbid}", string.Empty));
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, info);

                bool? check = await response.Content.ReadFromJsonAsync<bool>();

                if (check != null && check2 != null)
                {
                    isUserDataAccepted = (bool)check && (bool)check2;
                }

                if (isUserDataAccepted)
                {
                    await Shell.Current.GoToAsync($"..");
                }
                else
                {
                    Logininfo = "Пользователь уже зарегистрирован";
                }
            }
            else
            {
                Logininfo = "Заполнены не все поля";
            }
        }

    } 
}
