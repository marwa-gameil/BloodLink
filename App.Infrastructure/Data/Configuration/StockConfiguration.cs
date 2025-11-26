using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace App.Infrastructure.Data.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(stock => stock.Id);
            builder
               .HasOne(s => s.BloodBank) //every stock has one blood bank
               .WithMany() // blood bank has many stocks
               .HasForeignKey(s => s.BloodBankId) //foreign key in stock table link between stock and blood bank
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
