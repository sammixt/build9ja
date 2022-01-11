using System;
namespace build9ja.core.Entities
{
	public class Staff : BaseEntity
	{
		public Staff()
		{
		}

		public string StaffId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Sex { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string ImageLocation { get; set; }

		public string Status { get; set; }

		public StaffContact Contact { get; set; }
	}
}

