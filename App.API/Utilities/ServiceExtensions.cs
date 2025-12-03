using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
using App.Domain.Models;
using App.Application.Utilities;
using App.Infrastructure.Utilities;
using App.Infrastructure.Data;
using App.Infrastructure;

namespace App.API.Utilities;

public static class ServiceExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContextPoolConfiguration(configuration);
        services.AddHttpClient();


        services.AddIdentityConfiguration(configuration);
        services.AddAuthentication();
        services.AddAuthorization();
       
        services.AddCorsConfiguration();
        services.AddIISIntegrationConfiguration();
        services.AddControllers();
        services.AddServices();
        services.AddRepositories();
        return services;
    }

    public static IServiceCollection AddDbContextPoolConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        /* ------- Register Identity ------- */
        services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        /* ------- Get Default User Data from appsettings.json ------- */
        var defaultUserModel = configuration.GetSection("DefaultUserModel");
        if (defaultUserModel.Exists())
            services.Configure<User>(defaultUserModel);
        return services;
    }

    // public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var jwtSettings = configuration.GetSection("JwtSettings");
    //     services.AddAuthentication(options =>
    //     {
    //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //     })
    //     .AddJwtBearer(options =>
    //     {
    //         options.TokenValidationParameters = new TokenValidationParameters
    //         {
    //             ValidateIssuer = true,
    //             ValidateAudience = true,
    //             ValidateLifetime = true,
    //             ValidateIssuerSigningKey = true,
    //             ValidIssuer = jwtSettings["Issuer"],
    //             ValidAudience = jwtSettings["Audience"],
    //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
    //         };
    //     });
    //     return services;
    // }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
        services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                );
            }
        );

    public static IServiceCollection AddIISIntegrationConfiguration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {});
}
