using ChatClient.ViewModels;
using ChatClient.Views;
using Microsoft.Extensions.Logging;

namespace ChatClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<AllChatsPage>();
            builder.Services.AddTransient<ChatView>();
           

            builder.Services.AddTransient<ProfilePage>();

            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<RegisterPageViewModel>();
            builder.Services.AddTransient<AllChatsViewModel>();
            builder.Services.AddTransient<ChatViewModel>();

            builder.Services.AddTransient<ProfilePageViewModel>();

            return builder.Build();
        }
    }
}
