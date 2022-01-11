using System;
namespace build9ja.core.Entities
{
	public class StaffCredential : BaseEntity
	{
		public StaffCredential()
		{
		}

		public string StaffId { get; set; }

		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public string PasswordResetCypher { get; set; }

		public string Permissions { get; set; }

		public bool IsPasswordReset { get; set; }

		public DateTime LastLoginDate { get; set; }
	}
}

