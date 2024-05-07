using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class ProductWithFiltersForCountSpecificication : BaseSpecification<Product>
    {
         public ProductWithFiltersForCountSpecificication(string category,PaginationSpecification pagination) 
            : base(x => x.Category.Name.ToLower().Equals(category.ToLower()) &&
             (string.IsNullOrEmpty(pagination.Search) || x.ProductName.ToLower().Contains(pagination.Search)) &&
                (string.IsNullOrEmpty(pagination.Brand) || x.Brand.BrandName.Contains(pagination.Brand)) &&
                (!pagination.Amount.HasValue || x.BasePrice == pagination.Amount)
            ){
           
        }

    }
}