namespace ChatClient.Views;

public partial class EditSubsView : ContentPage
{
    public EditSubsView()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
       await viewModel.GetData();
        // base.OnAppearing();

    }
}
