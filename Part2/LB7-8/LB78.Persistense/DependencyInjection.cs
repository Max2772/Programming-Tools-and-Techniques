using LB78.Persistense.Data;
using LB78.Persistense.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LB78.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddPersistenceFake(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        DbContextOptions options)
    {
        services.AddPersistence()
            .AddSingleton<AppDbContext>(
                new AppDbContext((DbContextOptions<AppDbContext>)options));
        return services;
    }
}
