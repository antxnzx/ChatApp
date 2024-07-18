namespace ChatClient.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        viewModel.User = App.UserDetails;
        viewModel.RefreshData();
       // base.OnAppearing();
    }

   
}