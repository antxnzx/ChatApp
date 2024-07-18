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

namespace ChatClient.ViewModels
{
    public partial class EditSubsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<UserInfo> users;
        [ObservableProperty]
        ObservableCollection<UserSubs> subs;
        HttpClient client;
        [ObservableProperty]
        UserSubs subscription;
        [ObservableProperty]
        UserInfo userinfo;
        public EditSubsViewModel()
        {
            client = App.httpClient;
            users = [];
            subs = [];
        }

        public async Task GetData()
        {
            Users.Clear();
            Subs.Clear();
            Uri uri = new Uri(string.Format($"http://localhost:5000/users", string.Empty));

            if (client != null)
            {
                List<UserInfo>? usersfromdb = await client.GetFromJsonAsync<List<UserInfo>>(uri);
                if (usersfromdb != null)
                {
                    foreach (var user in usersfromdb)
                    {
                        Users.Add(user);
                    }
                }
            }
            uri = new Uri(string.Format($"http://localhost:5000/subs?login={App.UserDetails.Login}", string.Empty));

            if (client != null)
            {
                List<UserSubs>? subsfromdb = await client.GetFromJsonAsync<List<UserSubs>>(uri);
                if (subsfromdb != null)
                {
                    App.UserDetails.Subscriptions.Clear();
                    foreach (var user in subsfromdb)
                    {
                        App.UserDetails.Subscriptions.Add(user);
                    }
                }
            }
            foreach (var sub in App.UserDetails.Subscriptions)
            {
                Subs.Add(sub);
            }
        }

        [RelayCommand]
        async Task AddSub()
        {
            if (Userinfo != null)
            {

                UserSubs sub = new UserSubs();
                sub.UserLogin = App.UserDetails.Login;
                sub.Subscription = Userinfo.UserLogin;
                Uri uri = new Uri(string.Format($"http://localhost:5000/addsub", string.Empty));
                if (client != null)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, sub);
                    bool? check = await response.Content.ReadFromJsonAsync<bool>();

                    if (check == true)
                    {
                        await GetData();
                    }
                }
            }
        }

        [RelayCommand]
        async Task RemoveSub()
        {
            if (subscription != null)
            {
                Uri uri = new Uri(string.Format($"http://localhost:5000/removesub", string.Empty));
                if (client != null)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, Subscription);
                    bool? check = await response.Content.ReadFromJsonAsync<bool>();

                    if (check == true)
                    {
                        await GetData();
                    }
                }
            }
        }
    }
}
