namespace ChatClient.Views;

public partial class AllChatsPage : ContentPage
{
	public AllChatsPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        await viewModel.GetAllUsers();
        //base.OnAppearing();

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        users.SelectedItem = null;
    }
}