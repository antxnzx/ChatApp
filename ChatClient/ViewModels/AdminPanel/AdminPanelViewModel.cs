using ChatClient.Views;
using CommunityToolkit.Mvvm.Input;

namespace ChatClient.ViewModels.AdminPanel
{
    public partial class AdminPanelViewModel: BaseViewModel
    {
        [RelayCommand]
        async Task GoToUserInfoPage()
        {
            await Shell.Current.GoToAsync($"{nameof(UserInfoTablePage)}");
        }
        [RelayCommand]
        async Task GoToUserLoginInfoPage()
        {
            await Shell.Current.GoToAsync($"{nameof(UserLoginInfoPage)}");

        }
        [RelayCommand]
        async Task GoToUserSubsPage()
        {
            await Shell.Current.GoToAsync($"{nameof(UserSubsTablePage)}");

        }
    }
}
