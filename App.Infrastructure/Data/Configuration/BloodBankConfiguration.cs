using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BloodBankConfiguration : IEntityTypeConfiguration<BloodBank>
{
    public void Configure(EntityTypeBuilder<BloodBank> builder)
    {
        builder.HasKey(bb => bb.UserId);

        builder
            .HasOne(bb => bb.User)
            .WithOne(u => u.BloodBank)
            .HasForeignKey<BloodBank>(bb => bb.UserId)
            .IsRequired();
    }
}
