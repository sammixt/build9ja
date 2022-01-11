using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class VendorCredential : BaseEntity
    {
        public VendorCredential()
        {
            
        }

        public string VendorId { get; set; }

		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public string PasswordResetCypher { get; set; }

		public string Permissions { get; set; }

		public bool IsPasswordReset { get; set; }

		public DateTime LastLoginDate { get; set; }
    }
}