namespace ChatClient.Views;

public partial class UserLoginInfoPage : ContentPage
{
	public UserLoginInfoPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        viewModel.GetData();
        // base.OnAppearing();
    }

    void CleanSelection()
    {
        Collectionview.SelectedItem = null;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        CleanSelection();
    }
}