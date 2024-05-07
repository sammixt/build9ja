using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> checkEmail(string email)
        {
            VendorSpecification vendorSpecification = new VendorSpecification(email.Trim().ToLower(), true);
            var staff = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(vendorSpecification);
            return staff != null;
        }

        public async Task<bool> checkUserName(string userName)
        {
            VendorCredentialSpecification credentialSpecification = new VendorCredentialSpecification(userName);
            var user = await _unitOfWork.Repository<VendorCredential>().GetEntityWithSpec(credentialSpecification);
            return user != null;
        }

        public async Task<int> CreateBankInfo(VendorBankInfo bankInfo)
        {
              _unitOfWork.Repository<VendorBankInfo>().Add(bankInfo);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> CreateVendor(Vendor vendor)
        {
              _unitOfWork.Repository<Vendor>().Add(vendor);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public  async Task<int> getVendorsCount(PaginationSpecification specs)
        {
            VendorSpecification vendorSpecification = new VendorSpecification(specs, true);
            var vendor = await _unitOfWork.Repository<Vendor>().CountAsync(vendorSpecification);
            return vendor;
        }

        public  async Task<int> getCount()
        {
            VendorSpecification vendorSpecification = new VendorSpecification();
            var vendor = await _unitOfWork.Repository<Vendor>().CountAsync(vendorSpecification);
            return vendor;
        }

        public async Task<Vendor> getVendorById(long staffId)
        {
             VendorSpecification vendorSpecification = new VendorSpecification(staffId);
              var vendor = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(vendorSpecification);
            return vendor;
        }

        public async Task<Vendor> getVendorById(string staffId)
        {
            VendorSpecification vendorSpecification = new VendorSpecification(staffId);
              var vendor = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(vendorSpecification);
            return vendor;
        }

        public async Task<IReadOnlyList<Vendor>> getVendorsDataTable(DataTableRequestSpecification spec)
        {
            VendorSpecification credentialSpecification = new VendorSpecification(spec);
            var staffs = await _unitOfWork.Repository<Vendor>().ListAsync(credentialSpecification);
            return staffs;
        }

        public async Task<int> createVendorCredential(VendorCredential vendorCredential)
        {
             _unitOfWork.Repository<VendorCredential>().Add(vendorCredential);
            int created =  await _unitOfWork.Complete();
            return created;
        }

        public async Task<VendorCredential> getVendorCredentialByUserName(string username)
        {
            VendorCredentialSpecification spec = new VendorCredentialSpecification(username);
            var vendor = await _unitOfWork.Repository<VendorCredential>().GetEntityWithSpec(spec);
            return vendor;
        }

        public async Task<IReadOnlyList<Vendor>> getVendors(PaginationSpecification specs)
        {
            VendorSpecification credentialSpecification = new VendorSpecification(specs);
            var staffs = await _unitOfWork.Repository<Vendor>().ListAsync(credentialSpecification);
            return staffs;
        }

         public async Task<IReadOnlyList<Vendor>> getVendors()
        {
            VendorSpecification credentialSpecification = new VendorSpecification();
            var staffs = await _unitOfWork.Repository<Vendor>().ListAsync(credentialSpecification);
            return staffs;
        }

        public async Task<string> UpdateBankInfo(long id,VendorBankInfo bankInfo)
        {
            VendorBankInfoSpecification specification = new VendorBankInfoSpecification(id);
            var vendorBankInfo = await _unitOfWork.Repository<VendorBankInfo>().GetEntityWithSpec(specification);
            if (vendorBankInfo == null) return "99";
            vendorBankInfo.AccountName = bankInfo.AccountName??vendorBankInfo.AccountName;
            vendorBankInfo.AccountNumber = bankInfo.AccountNumber??vendorBankInfo.AccountNumber;
            vendorBankInfo.BankName = bankInfo.BankName??vendorBankInfo.BankName;
            _unitOfWork.Repository<VendorBankInfo>().Update(vendorBankInfo);
            int output = await _unitOfWork.Complete();
            if(output > 0) return "00";
            return "ER";
        }

        public async Task<VendorBankInfo> GetBankInfo(string sellerId){
             VendorBankInfoSpecification specification = new VendorBankInfoSpecification(sellerId);
            var vendorBankInfo = await _unitOfWork.Repository<VendorBankInfo>().GetEntityWithSpec(specification);
            return vendorBankInfo;
        }

        public async Task<VendorBankInfo> GetBankInfo(long sellerId){
             VendorBankInfoSpecification specification = new VendorBankInfoSpecification(sellerId);
            var vendorBankInfo = await _unitOfWork.Repository<VendorBankInfo>().GetEntityWithSpec(specification);
            return vendorBankInfo;
        }

        public async Task<string> UpdateVendor(long vendorId,Vendor vendor)
        {
            VendorSpecification spec = new VendorSpecification(vendorId);
            var model = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(spec);
            if(model == null) return "99";
            model.FirstName = vendor.FirstName??model.FirstName;
            model.LastName = vendor.LastName??model.LastName;
            model.Address = vendor.Address??model.Address;
            model.CacNumber = vendor.CacNumber??model.CacNumber;
            model.City = vendor.City??model.City;
            model.Company = vendor.Company??model.Company;
            model.Description = vendor.Description??model.Description;
            model.PhoneNumber = vendor.PhoneNumber??model.PhoneNumber;
            model.Email = vendor.Email??model.Email;
            model.State = vendor.State??model.State;
            model.TaxNumber = vendor.TaxNumber??model.TaxNumber;
            _unitOfWork.Repository<Vendor>().Update(model);
            int output = await _unitOfWork.Complete();
            if(output > 0) return "00";
            return "ER";
        }

        public async Task<string> UpdateVendor(string sellerId,Vendor vendor)
        {
            VendorSpecification spec = new VendorSpecification(sellerId);
            var model = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(spec);
            if(model == null) return "99";
            model.FirstName = vendor.FirstName??model.FirstName;
            model.LastName = vendor.LastName??model.LastName;
            model.Address = vendor.Address??model.Address;
            model.CacNumber = vendor.CacNumber??model.CacNumber;
            model.City = vendor.City??model.City;
            model.Company = vendor.Company??model.Company;
            model.Description = vendor.Description??model.Description;
            model.PhoneNumber = vendor.PhoneNumber??model.PhoneNumber;
            model.Email = vendor.Email??model.Email;
            model.State = vendor.State??model.State;
            model.TaxNumber = vendor.TaxNumber??model.TaxNumber;
            model.Status = vendor.Status;
            _unitOfWork.Repository<Vendor>().Update(model);
            int output = await _unitOfWork.Complete();
            if(output > 0) return "00";
            return "ER";
        }

        public async Task<string> UpdateVendorStatus(long vendorId,string status)
        {
            VendorSpecification spec = new VendorSpecification(vendorId);
            var model = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(spec);
            if(model == null) return "99";
             model.Status = status??model.TaxNumber;
            _unitOfWork.Repository<Vendor>().Update(model);
            int output = await _unitOfWork.Complete();
            if(output > 0) return "00";
            return "ER";
        }

        public async Task<VendorBankInfo> GetVendorBankInfo(string vendorId){
            VendorBankInfoSpecification spec = new VendorBankInfoSpecification(vendorId);
            var vendorBankInfo = await _unitOfWork.Repository<VendorBankInfo>().GetEntityWithSpec(spec);
            return vendorBankInfo;
        }

        public async Task<int> AddOrUpdateVendorBankInfo(VendorBankInfo bankInfo)
        {
            VendorBankInfoSpecification spec = new VendorBankInfoSpecification(bankInfo.SellerId);
             var vendorBankInfo = await _unitOfWork.Repository<VendorBankInfo>().GetEntityWithSpec(spec);
             if(vendorBankInfo != null){
                 vendorBankInfo.AccountName = bankInfo.AccountName ?? vendorBankInfo.AccountName;
                 vendorBankInfo.AccountNumber = bankInfo.AccountNumber ?? vendorBankInfo.AccountNumber;
                 vendorBankInfo.BankName = bankInfo.BankName ?? vendorBankInfo.BankName;
                  _unitOfWork.Repository<VendorBankInfo>().Update(vendorBankInfo);
             }else{
                 _unitOfWork.Repository<VendorBankInfo>().Add(bankInfo);
             }

             return await _unitOfWork.Complete();
        }
    }
    //create vendor
    //update vendor details
    //update vendor status
    //create bank info
    //update bank info
    //get vendor info by vendor Id
    //get paginated vendor list
    //get vendor count
    //check email

    //check username
    //get vendor credential by username;
}