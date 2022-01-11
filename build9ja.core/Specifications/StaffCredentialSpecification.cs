using System;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
	public class StaffCredentialSpecification : BaseSpecification<StaffCredential>
	{
		public StaffCredentialSpecification()
		{
		}

		public StaffCredentialSpecification(string username)
			: base(a => a.UserName.Trim().ToLower() == username.Trim().ToLower())
		{
		}

		public StaffCredentialSpecification(string staffId, bool byStaffId)
			: base(a => a.StaffId.Trim().ToLower() == staffId.Trim().ToLower())
		{
		}

		public StaffCredentialSpecification(string resetlink, bool byStaffId, bool resetLink)
			: base(a => a.PasswordResetCypher.Trim().ToLower() == resetlink.Trim().ToLower())
		{
		}
	}
}

