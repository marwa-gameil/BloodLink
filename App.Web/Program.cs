using App.Application.Interfaces;
using App.Application.Services;
using App.Application.Utilities;
using App.Domain.Models;
using App.Infrastructure.Data;
using App.Infrastructure.Utilities;
using App.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace App.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Env.Load();
            builder.Configuration.AddEnvironmentVariables();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            // -------------------------------
            // Identity DbContext (Web Layer)
            // -------------------------------
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // -------------------------------
            // Infrastructure DbContext (Business Layer)
            // -------------------------------
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // -------------------------------
            // Application Services & Repositories
            // -------------------------------
            builder.Services.AddServices();
            builder.Services.AddRepositories();

            // -------------------------------
            // HttpClient
            // -------------------------------
            builder.Services.AddHttpClient<Services.BloodBankService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5234");
            });

            builder.Services.AddHttpClient<HospitalServices>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5234");
            });

            builder.Services.AddHttpClient<UserWebService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5234");
            });
            builder.Services.AddScoped<AuthWebService>();
            builder.Services.AddScoped<ICookieAuthService, CookieAuthService>();

            // -------------------------------
            // MVC + Razor Pages
            // -------------------------------
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // -------------------------------
            // Seed Default Admin User & Roles
            // -------------------------------
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                // Seeding roles
                string[] roles = new[] { "Admin", "Hospital", "BloodBank" };
                foreach (var role in roles)
                {
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    }
                }

                // Seeding default admin user
          
            }

            // -------------------------------
            // Middleware
            // -------------------------------
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // -------------------------------
            // Routes
            // -------------------------------
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}
