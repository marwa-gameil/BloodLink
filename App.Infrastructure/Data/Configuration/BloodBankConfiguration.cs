using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace App.Infrastructure.Data.Configuration
{
    public class BloodBankConfiguration : IEntityTypeConfiguration<BloodBank>
    {
        public void Configure(EntityTypeBuilder<BloodBank> builder)
        {
            builder.HasKey(x => x.UserId);


            builder
                .HasOne(bb => bb.User)
                .WithOne(u => u.BloodBank)
                .HasForeignKey<BloodBank>(bb => bb.UserId);
        }
    }
}
