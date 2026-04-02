using Microsoft.Extensions.Logging;
using LB6.Pages;
using LB6.Services.Database;
using LB6.Services.Http;

namespace LB6;

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
        
        builder.Services.AddTransient<IDbService, SQLiteService>();
        builder.Services.AddTransient<SQLitePage>();

        builder.Services.AddHttpClient<IRateService, RateService>(opt =>
        {
            opt.BaseAddress = new Uri("https://api.nbrb.by/");
        });
        builder.Services.AddTransient<ConverterPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}