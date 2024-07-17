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
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<RegisterPage>();

            builder.Services.AddSingleton<ProfilePage>();

            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<RegisterPageViewModel>();

            builder.Services.AddSingleton<ProfilePageViewModel>();

            return builder.Build();
        }
    }
}
