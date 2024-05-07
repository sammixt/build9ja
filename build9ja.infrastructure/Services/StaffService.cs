using System;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{
	public class StaffService : IStaffService
	{
        private readonly IUnitOfWork _unitOfWork;
        public StaffService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;

        }


        public async Task<bool> checkEmail(string email)
        {
            StaffSpecification staffSpecification = new StaffSpecification(email.Trim().ToLower(), true);
            var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(staffSpecification);
            return staff != null;
        }

        public async Task<bool> checkUserName(string userName)
        {
            StaffCredentialSpecification credentialSpecification = new StaffCredentialSpecification(userName);
            var user = await _unitOfWork.Repository<StaffCredential>().GetEntityWithSpec(credentialSpecification);
            return user != null;
        }

        public async Task<bool> checkStaffHasLoginCredential(string staffId)
        {
            StaffCredentialSpecification credentialSpecification = new StaffCredentialSpecification(staffId,true);
            var user = await _unitOfWork.Repository<StaffCredential>().GetEntityWithSpec(credentialSpecification);
            return user != null;
        }

        public async Task<int> createStaff(Staff staff)
        {
             _unitOfWork.Repository<Staff>().Add(staff);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> createStaffCredential(StaffCredential staffCredential)
        {
             _unitOfWork.Repository<StaffCredential>().Add(staffCredential);
            int created =  await _unitOfWork.Complete();
            return created;
        }

        public async Task<StaffCredential> getStaffCredentialByUserName(string username)
        {
            StaffCredentialSpecification credentialSpecification = new StaffCredentialSpecification(username);
            StaffCredential staffCredential = await _unitOfWork.Repository<StaffCredential>().GetEntityWithSpec(credentialSpecification);
            return staffCredential;
        }

        public async Task<IReadOnlyList<Staff>> getStaffs(PaginationSpecification specs)
        {
            StaffSpecification credentialSpecification = new StaffSpecification(specs);
            var staffs = await _unitOfWork.Repository<Staff>().ListAsync(credentialSpecification);
            return staffs;
        }

        public async Task<IReadOnlyList<Staff>> getStaffs()
        {
            StaffSpecification credentialSpecification = new StaffSpecification();
            var staffs = await _unitOfWork.Repository<Staff>().ListAsync(credentialSpecification);
            return staffs;
        }

        public async Task<int> getStaffsCount(PaginationSpecification specs)
        {
            StaffSpecification staffSpecification = new StaffSpecification(specs, true);
            var staffs = await _unitOfWork.Repository<Staff>().CountAsync(staffSpecification);
            return staffs;
        }

        public async Task<Staff> getStaffByStaffId(string staffId)
        {
            StaffSpecification staffSpecification = new StaffSpecification(staffId);
            var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(staffSpecification);
            return staff;
        }

        public async Task<string> updateStaff(string staffId,Staff staff)
        {
            StaffSpecification staffSpecification = new StaffSpecification(staffId);
            var staffDetail = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(staffSpecification);
            if (staffDetail == null) return "99";
            staffDetail.Contact.Address = staff.Contact.Address?? staffDetail.Contact.Address;
            staffDetail.Contact.AltPhoneNumber = staff.Contact.AltPhoneNumber ?? staffDetail.Contact.AltPhoneNumber;
            staffDetail.Contact.City = staff.Contact.City ?? staffDetail.Contact.City;
            staffDetail.Contact.Country = staff.Contact.Country ?? staffDetail.Contact.Country;
            staffDetail.Contact.PhoneNumber = staff.Contact.PhoneNumber ?? staffDetail.Contact.PhoneNumber;
            staffDetail.Contact.State = staff.Contact.State ?? staffDetail.Contact.State;
            staffDetail.FirstName = staff.FirstName ?? staffDetail.FirstName;
            staffDetail.LastName = staff.LastName ?? staffDetail.LastName;
            staffDetail.Status = staff.Status ?? staffDetail.Status;
            staffDetail.DateOfBirth = staff.DateOfBirth ;
            _unitOfWork.Repository<Staff>().Update(staffDetail);

            await _unitOfWork.Complete();
            return "00";
           
        }
        //get by staffId
    }
}

