using System;
using build9ja.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace build9ja.infrastructure.Data.Config
{
	public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
	{
		public PermissionConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.PermissionName).HasMaxLength(250);
            builder.Property(p => p.Status).HasMaxLength(20);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.CreatedbyUsername).HasMaxLength(250);
        }
    }
}

