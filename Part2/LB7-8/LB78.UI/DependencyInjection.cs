using LB78.UI.Pages;
using LB78.UI.ViewModels;

namespace LB78.UI;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPages(this IServiceCollection services)
    {
        services
            .AddSingleton<SushiSetWindow>()
            .AddTransient<SushiDetailsWindow>()
            .AddTransient<AddOrEditSushiSetWindow>()
            .AddTransient<AddOrEditSushiWindow>();
        return services;
    }

    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services
            .AddSingleton<SushiSetViewModel>()
            .AddTransient<SushiDetailsViewModel>()
            .AddTransient<AddOrEditSushiSetViewModel>()
            .AddTransient<AddOrEditSushiViewModel>();
        return services;
    }
}
