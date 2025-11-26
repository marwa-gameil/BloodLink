using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Models;

namespace App.Infrastructure.Data.Configuration;


public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
             .HasOne(u => u.CreatedBy)
             .WithMany()
             .HasForeignKey(u => u.CreatedById);

    }
}
