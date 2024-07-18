using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ChatClient.Models;
using ChatClient.Views;
using ChatClient.ViewModels;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;



namespace ChatClient.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? emailLog = "";
        [ObservableProperty]
        private string? password = "";
        [ObservableProperty]
        private string? logininfo = "";

        private bool isUserDataAccepted = false;

        HttpClient client;

        public LoginPageViewModel()
        {
            App.httpClient = new HttpClient();
            client = App.httpClient;

        }

        [RelayCommand]
        async Task Login()
        {
            Uri uri = new Uri(string.Format($"http://localhost:5000/login", string.Empty));
            if (!string.IsNullOrWhiteSpace(EmailLog)
                && !string.IsNullOrWhiteSpace(Password))
            {
                Logininfo data = new(EmailLog, Password);
                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, data);
                    bool? check = await response.Content.ReadFromJsonAsync<bool>();
                    if (check != null)
                    {
                        isUserDataAccepted = (bool)check;

                    }

                    if (isUserDataAccepted)
                    {
                        UserBasicInfo basicInfo = new()
                        {
                            Login = EmailLog
                        };
                        await GetInformation(basicInfo);

                        if (Preferences.ContainsKey(nameof(App.UserDetails)))
                        {
                            Preferences.Remove(nameof(App.UserDetails));
                        }
                        string userDetailStr = JsonConvert.SerializeObject(basicInfo);
                        Preferences.Set(nameof(App.UserDetails), userDetailStr);
                        App.UserDetails = basicInfo;

                        App.hubConnection = new HubConnectionBuilder()
                                                .WithUrl("http://localhost:5000/chat")
                                                .WithAutomaticReconnect()
                                                .Build();
                        try
                        {
                            await App.hubConnection.StartAsync();
                            await App.hubConnection.InvokeAsync("NotifyUsers", basicInfo.Login);
                        }
                        catch (Exception)
                        {

                        }
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        Logininfo = "Неверные данные для входа";
                    }
                }
                catch (Exception)
                {
                    Logininfo = "Нет связи с сервером";

                }

            }
            else
            {
                Logininfo = "Поля не заполнены";
            }
        }
        async Task GetInformation(UserBasicInfo basicInfo)
        {
            Uri uri = new Uri(string.Format($"http://localhost:5000/profile?login={basicInfo.Login}", string.Empty));

            UserInfo? userInfo = await client.GetFromJsonAsync<UserInfo>(uri);
            if (userInfo != null)
            {
                basicInfo.id = userInfo.Id;
                basicInfo.Surname = userInfo.Surname;
                basicInfo.Name = userInfo.Name;
                basicInfo.IsAdmin = userInfo.IsAdmin;
            }
            uri = new Uri(string.Format($"http://localhost:5000/subs?login={basicInfo.Login}", string.Empty));
            List<UserSubs>? userSubs = await client.GetFromJsonAsync<List<UserSubs>>(uri);
            if (userSubs != null)
            {
                basicInfo.Subscriptions.AddRange(userSubs);
            }
        }

        [RelayCommand]
        async Task Register()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        public void CleanFields()
        {
            EmailLog = "";
            Password = "";
            Logininfo = "";
            isUserDataAccepted = false;
        }
    }
}
