using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace build9ja.infrastructure.Data.Config
{
    public class VendorBankInfoConfiguration : IEntityTypeConfiguration<VendorBankInfo>
    {
        public void Configure(EntityTypeBuilder<VendorBankInfo> builder)
        {
            builder.Property(x => x.SellerId).HasMaxLength(20);
            builder.Property(x => x.BankName).HasMaxLength(150);
            builder.Property(x => x.AccountNumber).HasMaxLength(15);
            builder.Property(x => x.AccountName).HasMaxLength(100);
        }
    }
}