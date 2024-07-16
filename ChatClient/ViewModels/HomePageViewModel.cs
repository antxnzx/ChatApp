using ChatClient.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {

        public HomePageViewModel() 
        {

        }

        [RelayCommand]
        public async Task GoToTelegram()
        {
            try
            {
                Uri uri = new Uri("https://t.me/yarpgups");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {

            }
        }
        [RelayCommand]
        public async Task GoToVk()
        {
            try
            {
                Uri uri = new Uri("https://vk.com/yar_pgups");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occurred. No browser may be installed on the device.
            }
        }

        
    }
}
