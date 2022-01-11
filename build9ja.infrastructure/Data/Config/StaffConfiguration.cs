using System;
using build9ja.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace build9ja.infrastructure.Data.Config
{
	public class StaffConfiguration : IEntityTypeConfiguration<Staff>
	{
		public StaffConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.OwnsOne(o => o.Contact, a =>
            {
                a.WithOwner();
            });
           // builder.HasKey(e => e.StaffId);
        }
    }
}

