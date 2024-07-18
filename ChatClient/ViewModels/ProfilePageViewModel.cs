using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChatClient.Views;
using ChatClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private UserBasicInfo? user;
        [ObservableProperty]
        private ObservableCollection<UserSubs> subs;
       
    
        public ProfilePageViewModel() 
        {
            Subs = new ObservableCollection<UserSubs>();
        }

        [RelayCommand]
        async Task SignOut()
        {
            await App.hubConnection.InvokeAsync("NotifyUsers", App.UserDetails.Login, "не в сети");
            await App.hubConnection.StopAsync();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            App.UserDetails = null;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            
        }

        [RelayCommand]
        async Task Edit()
        {
            await Shell.Current.GoToAsync($"{nameof(EditSubsView)}");
        }

        
        public void RefreshData()
        {
            Subs.Clear();
            foreach (var item in User.Subscriptions)
            {
                Subs.Add(item);
            }
        }
    }

}
