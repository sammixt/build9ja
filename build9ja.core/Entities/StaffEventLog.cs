using System;
namespace build9ja.core.Entities
{
	public class StaffEventLog : BaseEntity
	{
		public StaffEventLog()
		{
		}

		public string StaffId { get; set; }

		public string UserName { get; set; }

		public string EventType { get; set; }

		public string Action { get; set; }

		public DateTime EventDate { get; set; }

		public string ClientIpAddress { get; set; }
	}
}

