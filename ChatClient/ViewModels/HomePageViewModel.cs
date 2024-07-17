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
        [ObservableProperty]
        private bool admin = false;
        public HomePageViewModel() 
        {
            admin = App.UserDetails.IsAdmin == 1;
        }

        

        
    }
}
