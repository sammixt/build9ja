using System;
using System.ComponentModel.DataAnnotations;

namespace build9ja.core.Entities
{
	public class BaseEntity
	{
		public BaseEntity()
		{
		}
		[Key]
		public long Id { get; set; }

		public DateTime DateCreated {get; set;} = DateTime.UtcNow;
	}
}

