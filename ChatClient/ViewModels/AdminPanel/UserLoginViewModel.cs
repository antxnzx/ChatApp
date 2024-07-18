using ChatClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Net.Http.Json;


namespace ChatClient.ViewModels.AdminPanel
{
    public partial class UserLoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<UserLoginInfo> loginInfos;
        [ObservableProperty]
        UserLoginInfo? loginInfo;
        HttpClient client;
        Uri uri = new Uri(string.Format($"http://localhost:5000/UserLoginInfo", string.Empty));
        public UserLoginViewModel()
        {
            LoginInfos = new ObservableCollection<UserLoginInfo>();
            client = App.httpClient;
        }

        [RelayCommand]
        async Task AddToDb()
        {
            if (client != null)
            {
                 
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, LoginInfo);
                bool? check = await response.Content.ReadFromJsonAsync<bool>();
                if (check == true)
                {
                    GetData();
                }
            }
        }

        [RelayCommand]
        async Task DeleteFromDb()
        {
            if (client != null)
            {

                HttpResponseMessage response = await client.DeleteAsync($"{uri}/{LoginInfo.Id}");
                bool? check = await response.Content.ReadFromJsonAsync<bool>();
                if (check == true)
                {
                    GetData();
                }
            }
        }

        [RelayCommand]
        async Task UpdateToDb()
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(uri, LoginInfo);
            bool? check = await response.Content.ReadFromJsonAsync<bool>();
            if (check == true)
            {
                GetData();
            }
        }

        public async void GetData()
        {
            
            if (client != null)
            {
                List<UserLoginInfo>? usersfromdb = await client.GetFromJsonAsync<List<UserLoginInfo>>(uri);
                if (usersfromdb != null)
                {
                    LoginInfos.Clear();
                    foreach (UserLoginInfo user in usersfromdb)
                    {
                        LoginInfos.Add(user);

                    }
                }
            }
            LoginInfo = null;
        }
    }
}
