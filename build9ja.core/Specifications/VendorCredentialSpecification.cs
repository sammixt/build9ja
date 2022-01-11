using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class VendorCredentialSpecification : BaseSpecification<VendorCredential>
    {
        public VendorCredentialSpecification()
		{
		}

		public VendorCredentialSpecification(string username)
			: base(a => a.UserName.Trim().ToLower() == username.Trim().ToLower())
		{
		}

		public VendorCredentialSpecification(string vendorId, bool byStaffId)
			: base(a => a.VendorId.Trim().ToLower() == vendorId.Trim().ToLower())
		{
		}

		public VendorCredentialSpecification(string resetlink, bool byStaffId, bool resetLink)
			: base(a => a.PasswordResetCypher.Trim().ToLower() == resetlink.Trim().ToLower())
		{
		}
    }
}