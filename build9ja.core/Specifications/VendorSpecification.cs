using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class VendorSpecification : BaseSpecification<Vendor>
    {
        public VendorSpecification(){}
        public VendorSpecification(long id): base (x => x.Id == id){}
        public VendorSpecification(string vendorId) : base (x => x.SellerId == vendorId){}
        public VendorSpecification(string email, bool isEmail)
			: base(a => a.Email== email){}
            
        public VendorSpecification(PaginationSpecification pagination)
			: base(x => (string.IsNullOrEmpty(pagination.Search) || x.FirstName.ToLower().Contains(pagination.Search)))
        {
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
		}

		public VendorSpecification(PaginationSpecification pagination, bool hasCount)
			: base(x => (string.IsNullOrEmpty(pagination.Search) || x.FirstName.ToLower().Contains(pagination.Search)))
		{}

        public VendorSpecification(DataTableRequestSpecification spec)
		: base(x => (string.IsNullOrEmpty(spec.SearchValue) || x.Company.Contains(spec.SearchValue)
		|| x.TaxNumber.Contains(spec.SearchValue) || x.CacNumber.Contains(spec.SearchValue))){
			ApplyPaging(((spec.Skip / spec.PageSize) * spec.PageSize),spec.PageSize);
		}
    }

    public class VendorBankInfoSpecification : BaseSpecification<VendorBankInfo>
    {
        public VendorBankInfoSpecification(){}
        public VendorBankInfoSpecification(long id): base(x => x.Id == id){}
        public VendorBankInfoSpecification(string sellerId): base(x => x.SellerId == sellerId){}

    }
}