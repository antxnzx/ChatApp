using Newtonsoft.Json;
using ChatClient.Models;
using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModels
{
    public class LoadingPageViewModel
    {
     
        public LoadingPageViewModel() 
        {
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails),"");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                //navigate to login page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }
    }
}
