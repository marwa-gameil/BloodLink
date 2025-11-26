using App.Application.Interfaces;
using App.Application.Services;
using FoodRescue.Application.Utilities;
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
     
        services.AddScoped<IBloodBankService, BloodBankService>();
        services.AddScoped<ICurrentLoggedInUser,CurrentLoggedInUser >();
        services.AddScoped<ICookieAuthService, CookieAuthService>();
        services.AddScoped<IHospitalService, HospitalService>();
        services.AddScoped<IUserService, UserService>();    
        return services;
    }
}
