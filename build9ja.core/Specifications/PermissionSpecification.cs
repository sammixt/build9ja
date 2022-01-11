using System;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
	public class PermissionSpecification : BaseSpecification<Permission>
	{
		public PermissionSpecification()
		{
		}

		public PermissionSpecification(long id)
			: base(x => x.Id == id)
        {

        }

		public PermissionSpecification(string permissionName)
			: base(x => x.PermissionName.ToLower().Equals(permissionName.ToLower()))
        {
			
        }
	}
}

