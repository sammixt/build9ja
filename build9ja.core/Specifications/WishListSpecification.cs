using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class WishListSpecification : BaseSpecification<WishList>
    {
        public WishListSpecification()
        {
            AddInclude(x => x.Product);
        }

        public WishListSpecification(string email)
            : base(x => x.user == email)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Product.Category);
            AddInclude(x => x.Product.Brand);
            AddInclude(x => x.Product.Vendor);
            AddInclude(x => x.Product.ProductSpecification);
            AddInclude(x => x.Product.ProductVariations);
            AddInclude(x => x.Product.ProductImage);
        }

        public WishListSpecification(string email, long productId)
            : base(x => x.user == email && x.ProductId == productId)
        {
            AddInclude(x => x.Product);
        }

        public WishListSpecification(long id)
            :base(x => x.ProductId == id)
        {
            AddInclude(x => x.Product);
             AddInclude(x => x.Product.Category);
            AddInclude(x => x.Product.Brand);
            AddInclude(x => x.Product.Vendor);
            AddInclude(x => x.Product.ProductSpecification);
            AddInclude(x => x.Product.ProductVariations);
            AddInclude(x => x.Product.ProductImage);
        }

         public WishListSpecification(string email,PaginationSpecification pagination) 
            : base(x => x.user.ToLower().Equals(email.ToLower()) 
            ){
            AddInclude(x => x.Product);
             AddInclude(x => x.Product.Category);
            AddInclude(x => x.Product.Brand);
            AddInclude(x => x.Product.Vendor);
            AddInclude(x => x.Product.ProductSpecification);
            AddInclude(x => x.Product.ProductVariations);
            AddInclude(x => x.Product.ProductImage);
            AddOrderByDescending(x => x.DateCreated);
            ApplyPaging(pagination.PageSize * (pagination.PageIndex - 1), pagination.PageSize);
            //ApplyTake(take);
        }
    }
}