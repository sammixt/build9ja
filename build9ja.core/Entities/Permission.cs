using System;
namespace build9ja.core.Entities
{
	public class Permission : BaseEntity
	{
		public Permission()
		{
		}

		

		public string PermissionName { get; set; }

		public string Status { get; set; }

		public string CreatedbyUsername { get; set; }

		public string CreatedBy { get; set; }

	}
}

