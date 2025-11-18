using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using App.Domain.Models;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace App.API.Utilities;

public static class WebAppExtensions
{
    public static void Configure(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.Lifetime.ApplicationStarted.Register(async () => await app.PreLoadDefaultData());
    }

    private static async Task PreLoadDefaultData(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await roleManager.CreateRolesIfNotExist(["bloodbank", "hospital", "Admin"]);

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        User? user = scope.ServiceProvider.GetService<IOptions<User>>()?.Value;
        if(user is not null)await userManager.CreateUserIfNotExist(user, "Admin");
    }

    private static async Task CreateUserIfNotExist(this UserManager<User> userManager, User user, string role)
    {
        User? existingUser = await userManager.FindByIdAsync(user.Id.ToString());
        if(existingUser is not null) return;
        var result = await userManager.CreateAsync(user, user.PasswordHash!);
        if(!result.Succeeded)
        {
            foreach(var e in result.Errors) Console.WriteLine(e.Description);
        }
        await userManager.AddToRoleAsync(user, role);
    }

    private static async Task CreateRolesIfNotExist(this RoleManager<IdentityRole<Guid>> roleManager, string[] roles)
    {
        foreach(string role in roles)
        {
            if(await roleManager.RoleExistsAsync(role)) continue;
            var result = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            if(result.Succeeded) continue;
            foreach(var e in result.Errors) Console.WriteLine(e.Description);
        }
    }

}
