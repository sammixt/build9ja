using System;
using System.ComponentModel.DataAnnotations;

namespace build9ja.api.Dto
{
	public class StaffDto
	{
		public StaffDto()
		{
		}

		public string StaffId { get; set; }

		[Required(ErrorMessage = "Firstname is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Lastname is required")]
		public string LastName { get; set; }

		public string Sex { get; set; }

		[Required(ErrorMessage = "Sex is required")]
		public DateTime DateOfBirth { get; set; }

		public string ImageLocation { get; set; }

		public string Status { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		[RegularExpression("^\\d{7,15}$",ErrorMessage = "Phone number should be between 7 to 15 digits")]
		public string PhoneNumber { get; set; }

		public string AltPhoneNumber { get; set; }

		[Required(ErrorMessage ="Email address is required")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Country { get; set; }
	}
}

