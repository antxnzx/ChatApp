

using ChatClient.Models;

namespace ChatClient.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();		
	}

    protected override void OnAppearing()
    {
        viewModel.CleanFields();
        //base.OnAppearing();
    }

    
}