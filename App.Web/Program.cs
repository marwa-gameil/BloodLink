using App.Web.Data;
using App.Infrastructure.Data;
using App.Application.Utilities;
using App.Infrastructure.Utilities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using App.Application.Utilities;     
using App.Infrastructure.Utilities;
using App.Infrastructure.Data;


namespace App.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            // Register ApplicationDbContext for Identity
            builder.Services.AddDbContext<App.Web.Data.ApplicationDbContext>(options =>

                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Register Infrastructure ApplicationDbContext for business logic
            builder.Services.AddDbContext<App.Infrastructure.Data.ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<App.Web.Data.ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            // Register Application Services and Repositories
            builder.Services.AddServices();
            builder.Services.AddRepositories();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
