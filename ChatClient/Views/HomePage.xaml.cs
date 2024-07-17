namespace ChatClient.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		
	}

    protected override async void OnAppearing()
    {
		await viewModel.GetAllUsers();
		//base.OnAppearing();

    }
}