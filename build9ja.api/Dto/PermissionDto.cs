using System;
using System.ComponentModel.DataAnnotations;

namespace build9ja.api.Dto
{
	public class PermissionDto
	{
		public PermissionDto()
		{
		}
		public long Id { get; set; }

		[Required(ErrorMessage = "Permission Name is required")]
		public string PermissionName { get; set; }

		[Required(ErrorMessage = "Status is required")]
		public string Status { get; set; }

		public string CreatedbyUsername { get; set; }

		public string CreatedBy { get; set; }

		public DateTime dateCreate { get; set; }
	}
}

