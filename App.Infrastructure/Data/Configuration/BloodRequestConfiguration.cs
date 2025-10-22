using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace App.Infrastructure.Data.Configuration;


public class BloodRequestConfiguration : IEntityTypeConfiguration<BloodRequest>
{
    public void Configure(EntityTypeBuilder<BloodRequest> builder)
    {
           builder
          .HasOne(r => r.Hospital) //every request has one hospital
          .WithMany(h => h.BloodRequests) // hospital has many requests
          .HasForeignKey(r => r.HospitalId) //foreign key in request table link between request and hospital
          .OnDelete(DeleteBehavior.Restrict);

          builder
         .HasOne(r => r.BloodBank) //every request has one blood bank
         .WithMany(b => b.Requests)
         .HasForeignKey(r => r.BloodBankId) //foreign key in request table link between request and blood bank
        .OnDelete(DeleteBehavior.Restrict);
    }
}
