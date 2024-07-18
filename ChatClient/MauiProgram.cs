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
            builder.Services.AddScoped<LoginPage>();
            builder.Services.AddScoped<HomePage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<AllChatsPage>();
            builder.Services.AddTransient<ChatView>();
           

            builder.Services.AddScoped<ProfilePage>();

            builder.Services.AddScoped<LoginPageViewModel>();
            builder.Services.AddScoped<HomePageViewModel>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddSingleton<AllChatsViewModel>();
            builder.Services.AddTransient<ChatViewModel>();

            builder.Services.AddScoped<ProfilePageViewModel>();

            return builder.Build();
        }
    }
}
