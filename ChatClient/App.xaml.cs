using ChatClient.Models;

namespace ChatClient
{
    public partial class App : Application
    {
        public static UserBasicInfo ?UserDetails;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
