using ChatClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModels.AdminPanel
{
    public partial class UserSubsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<UserSubs> loginInfos;
        [ObservableProperty]
        UserSubs? loginInfo = new();
        HttpClient client;
        Uri uri = new Uri(string.Format($"http://localhost:5000/UserSubs", string.Empty));
        public UserSubsViewModel()
        {
            LoginInfos = new ObservableCollection<UserSubs>();
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
                List<UserSubs>? usersfromdb = await client.GetFromJsonAsync<List<UserSubs>>(uri);
                if (usersfromdb != null)
                {
                    LoginInfos.Clear();
                    foreach (UserSubs user in usersfromdb)
                    {
                        LoginInfos.Add(user);

                    }
                }
            }
            LoginInfo = new();
        }
    }
}
