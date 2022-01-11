using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace build9ja.infrastructure.Data.Config
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(x => x.SellerId).HasMaxLength(20);
            builder.Property(x => x.Company).HasMaxLength(350);
            builder.Property(x => x.TaxNumber).HasMaxLength(50);
            builder.Property(x => x.CacNumber).HasMaxLength(50);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.Email).HasMaxLength(250);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.City).HasMaxLength(100);
            builder.Property(x => x.State).HasMaxLength(50);
            builder.Property(x => x.Status).HasMaxLength(20);
        }
    }
}