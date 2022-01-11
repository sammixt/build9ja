using System;
using Microsoft.EntityFrameworkCore;
using build9ja.core;
using build9ja.core.Entities;
using System.Reflection;

namespace build9ja.infrastructure.Data
{
	public class AppDbContext : DbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Staff> Staffs { get; set; }
		public DbSet<StaffCredential> StaffCredentials { get; set; }
		public DbSet<StaffEventLog> StaffEventLogs { get; set; }
		public DbSet<Commission> Commissions {get; set;}
		public DbSet<Vendor> Vendors {get; set;}
		public DbSet<VendorBankInfo> vendorBankInfos {get; set;}
		public DbSet<VendorCredential> VendorCredentials { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}
	}
}

