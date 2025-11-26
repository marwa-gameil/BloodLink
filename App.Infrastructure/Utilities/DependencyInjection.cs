using App.Domain.Interfaces;
using App.Domain.Models;
using App.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<IBloodBankRepository, BloodBankRepository>();
        services.AddScoped<UserManager<User>, UserManager<User>>();
        services.AddScoped<SignInManager<User>, SignInManager<User>>();
        services.AddScoped<IHospitalRepository, HospitalRepository>();

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
