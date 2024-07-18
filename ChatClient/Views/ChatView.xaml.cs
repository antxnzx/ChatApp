namespace ChatClient.Views;

public partial class ChatView : ContentPage
{
	public ChatView()
	{
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        await viewModel.JoinChat();
        //base.OnAppearing();

    }

    protected override async void OnDisappearing()
    {
        await viewModel.RemoveFromChat();
    }
    
}