using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SpinningTrainer.Views;
using MauiIcons.FontAwesome;
using MauiIcons.Material;

namespace SpinningTrainer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit()
            .UseMaterialMauiIcons();

            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<ConnectionView>();
            builder.Services.AddSingleton<MainPageView>();
            
#if DEBUG
            builder.Logging.AddDebug();            
#endif
            return builder.Build();
        }
    }
}