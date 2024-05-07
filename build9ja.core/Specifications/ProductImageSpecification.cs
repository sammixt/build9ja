using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class ProductImageSpecification : BaseSpecification<ProductImage>
    {
         public ProductImageSpecification() : base(){}
        public ProductImageSpecification(long id) : base(x => x.Id == id){}

        public ProductImageSpecification(long id, bool byProduct) : base(x => x.ProductId == id){}
    }
}