using Microsoft.Maui.Controls;

namespace ChatClient.Views;

public partial class UserSubsTablePage : ContentPage
{
	public UserSubsTablePage()
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
        viewModel.LoginInfo = new();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        CleanSelection();
    }
}