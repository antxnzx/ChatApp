using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private UserBasicInfo? user;
       
    
        public ProfilePageViewModel() 
        {
            
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

        
    }

}
