using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification() : base(){}
        public ProductSpecification(long id) : base(x => x.Id == id){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
        }

         public ProductSpecification(int take, bool isTake) : base(){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
            AddOrderByDescending(x => x.DateCreated);
            ApplyTake(take);
        }

        public ProductSpecification(string category,PaginationSpecification pagination) 
            : base(x => x.Category.Name.ToLower().Equals(category.ToLower()) &&
             (string.IsNullOrEmpty(pagination.Search) || x.ProductName.ToLower().Contains(pagination.Search)) &&
                (string.IsNullOrEmpty(pagination.Brand) || x.Brand.BrandName.Contains(pagination.Brand)) &&
                (!pagination.Amount.HasValue || x.BasePrice == pagination.Amount)
            ){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
            AddOrderByDescending(x => x.DateCreated);
            ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
            //ApplyTake(take);
        }

         public ProductSpecification(bool isFeatured) : base(x => x.IsFeatured == isFeatured ){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
            AddOrderByDescending(x => x.DateCreated);
        }
        
        public ProductSpecification(string id) : base(x => x.ProductIdString.ToLower().Trim() == id){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
        }

        public ProductSpecification(long[] categoryId) : base(x => categoryId.Contains(x.CategoryId)){
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
        }

        public ProductSpecification(PaginationSpecification pagination)
			: base(x => (string.IsNullOrEmpty(pagination.Search) || x.ProductName.ToLower().Contains(pagination.Search)))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
			if (!string.IsNullOrEmpty(pagination.sort))
            {
                switch (pagination.sort)
                {
                    
                    case "prodName":
                        AddOrderBy(p => p.ProductName);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DateCreated);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;

                }
            }
		}
        public ProductSpecification(PaginationSpecification pagination,Brand brand)
			: base(x => x.BrandId == brand.Id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
			if (!string.IsNullOrEmpty(pagination.sort))
            {
                switch (pagination.sort)
                {
                    
                    case "prodName":
                        AddOrderBy(p => p.ProductName);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DateCreated);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;

                }
            }
		}

        public ProductSpecification(PaginationSpecification pagination,Category category)
			: base(x => x.CategoryId == category.Id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
			if (!string.IsNullOrEmpty(pagination.sort))
            {
                switch (pagination.sort)
                {
                    
                    case "prodName":
                        AddOrderBy(p => p.ProductName);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DateCreated);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;

                }
            }
		}

        public ProductSpecification(PaginationSpecification pagination,Vendor vendor)
			: base(x => x.VendorId == vendor.Id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
			ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
			if (!string.IsNullOrEmpty(pagination.sort))
            {
                switch (pagination.sort)
                {
                    
                    case "prodName":
                        AddOrderBy(p => p.ProductName);
                        break;
                    case "dateDesc":
                        AddOrderByDescending(p => p.DateCreated);
                        break;
                    default:
                        AddOrderBy(p => p.ProductName);
                        break;

                }
            }
		}

        public ProductSpecification(DataTableRequestSpecification spec)
		: base(x => (string.IsNullOrEmpty(spec.SearchValue) || x.ProductName.Contains(spec.SearchValue)
		|| x.Category.Name.Contains(spec.SearchValue) || x.Vendor.Company.Contains(spec.SearchValue))){
			AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Vendor);
            AddInclude(x => x.ProductSpecification);
            AddInclude(x => x.ProductVariations);
            AddInclude(x => x.ProductImage);
            AddOrderByDescending(x => x.Id);
            ApplyPaging(((spec.Skip / spec.PageSize) * spec.PageSize),spec.PageSize);
		}
    }
}