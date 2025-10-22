using Microsoft.Extensions.DependencyInjection;
using App.Domain.Interfaces;
using App.Infrastructure.Repositories;

namespace App.Infrastructure.Utilities;

public static class DependencyInjection
{
    /// <summary>
    ///     Inject all the repositories
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting repositories</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
