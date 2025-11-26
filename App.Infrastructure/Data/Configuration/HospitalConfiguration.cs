using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configuration
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(x => x.UserId);

            builder
                .HasOne(bb => bb.User)
                .WithOne(u => u.Hospital)
                .HasForeignKey<Hospital>(bb => bb.UserId);
        }
    }
}
