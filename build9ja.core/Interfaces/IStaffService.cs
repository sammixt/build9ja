using System;
using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
	public interface IStaffService
	{
		Task<bool> checkUserName(string userName);
		Task<bool> checkEmail(string email);
		Task<int> createStaff(Staff staff);
		Task<int> createStaffCredential(StaffCredential staffCredential);
		Task<StaffCredential> getStaffCredentialByUserName(string username);
		Task<IReadOnlyList<Staff>> getStaffs(PaginationSpecification specs);
		Task<int> getStaffsCount(PaginationSpecification specs);
		Task<Staff> getStaffByStaffId(string staffId);
		Task<string> updateStaff(string staffId, Staff staff);
        Task<IReadOnlyList<Staff>> getStaffs();
        Task<bool> checkStaffHasLoginCredential(string staffId);
    }

}

