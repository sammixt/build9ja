using System;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
	public class StaffEventLogSpecification : BaseSpecification<StaffEventLog>
	{
		public StaffEventLogSpecification()
		{
		}

		public StaffEventLogSpecification(string staffId)
			: base(x => x.StaffId.Trim().ToLower().Equals(staffId.ToLower().Trim()))
        {

        }
	}
}

