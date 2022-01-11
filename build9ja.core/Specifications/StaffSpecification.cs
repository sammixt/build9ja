using System;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
	public class StaffSpecification : BaseSpecification<Staff>
	{
		public StaffSpecification()
		{
		}

		public StaffSpecification(long id)
			: base(a => a.Id == id)
		{
		}

		public StaffSpecification(PaginationSpecification pagination)
			: base(x => (string.IsNullOrEmpty(pagination.Search) || x.FirstName.ToLower().Contains(pagination.Search)))
        {
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
			
		}

		public StaffSpecification(PaginationSpecification pagination, bool hasCount)
			: base(x => (string.IsNullOrEmpty(pagination.Search) || x.FirstName.ToLower().Contains(pagination.Search)))
		{

		}

		public StaffSpecification(string staffId)
			: base(a => a.StaffId.Trim() == staffId)
		{
		}

		public StaffSpecification(string email, bool isEmail)
			: base(a => a.Contact.EmailAddress == email)
		{
		}
	}
}

