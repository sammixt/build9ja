using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface IVendorService
    {
        //create vendor
        Task<int> CreateVendor(Vendor vendor);
        //update vendor details
         Task<string> UpdateVendor(long vendorId,Vendor vendor);
        //update vendor status
        Task<string> UpdateVendorStatus(long vendorId,string status);
        //create bank info
        Task<int> CreateBankInfo(VendorBankInfo bankInfo);
        //update bank info
        Task<string> UpdateBankInfo(long id,VendorBankInfo bankInfo);
        Task<VendorBankInfo> GetBankInfo(long sellerId);
        Task<VendorBankInfo> GetBankInfo(string sellerId);
        //get vendor info by vendor Id
        Task<Vendor> getVendorById(string staffId);
        Task<Vendor> getVendorById(long staffId);
        //get paginated vendor list
        Task<IReadOnlyList<Vendor>> getVendors(PaginationSpecification specs);
        //get vendor count
        Task<int> getVendorsCount(PaginationSpecification specs);
        //check username
        Task<bool> checkUserName(string userName);
        //check email
		Task<bool> checkEmail(string email);
        //get vendor credential by username;
        Task<VendorCredential> getVendorCredentialByUserName(string username);
        Task<int> createVendorCredential(VendorCredential vendorCredential);
        Task<string> UpdateVendor(string sellerId, Vendor vendor);
        Task<IReadOnlyList<Vendor>> getVendorsDataTable(DataTableRequestSpecification spec);
        Task<int> getCount();
        Task<IReadOnlyList<Vendor>> getVendors();
        Task<int> AddOrUpdateVendorBankInfo(VendorBankInfo bankInfo);
        Task<VendorBankInfo> GetVendorBankInfo(string vendorId);
    }
}