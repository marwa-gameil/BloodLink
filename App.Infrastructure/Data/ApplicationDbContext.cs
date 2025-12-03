using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Domain.Models;

namespace App.Infrastructure.Data;

public sealed class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options){}
  
    public DbSet<BloodRequest> BloodRequests { get; set; }
    public DbSet<BloodBank> BloodBanks { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    //public DbSet<User> Users { get; set; }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
