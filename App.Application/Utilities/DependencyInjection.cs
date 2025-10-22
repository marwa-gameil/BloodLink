using App.Application.Interfaces;
using App.Application.Services;
using App.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace App.Application.Utilities;

public static class DependencyInjection
{
    /// <summary>
    ///     Inject all the services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting services</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
     
        services.AddScoped<IRequestService, RequestService>();

        return services;
    }
}
