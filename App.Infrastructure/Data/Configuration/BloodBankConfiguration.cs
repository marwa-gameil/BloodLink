using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configuration
{
    public class BloodBankConfiguration : IEntityTypeConfiguration<BloodBank>
    {
        public void Configure(EntityTypeBuilder<BloodBank> builder)
        {
            
        }
    }
}
