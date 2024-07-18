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
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            App.UserDetails = null;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            
        }

        
    }

}
