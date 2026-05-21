using CommunityToolkit.Maui;
using LB78.Application;
using LB78.Persistense;
using LB78.Persistense.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace LB78.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        const string settingsStream = "LB78.UI.appsettings.json";

        var builder = MauiApp.CreateBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(settingsStream)
            ?? throw new InvalidOperationException($"Embedded resource '{settingsStream}' not found.");
        builder.Configuration.AddJsonStream(stream);

        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        string dataDirectory = FileSystem.Current.AppDataDirectory + "/";
        connStr = string.Format(connStr!, dataDirectory);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connStr)
            .Options;

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var useFake = bool.TryParse(builder.Configuration["UseFakeRepository"], out var fake) && fake;

        builder.Services.AddApplication();

        if (useFake)
            builder.Services.AddPersistenceFake();
        else
            builder.Services.AddPersistence(options);

        builder.Services
            .RegisterPages()
            .RegisterViewModels();

        if (!useFake)
        {
            DbInitializer
                .Initialize(builder.Services.BuildServiceProvider())
                .Wait();
        }

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
