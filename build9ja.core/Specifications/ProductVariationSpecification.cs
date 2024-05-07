using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class ProductVariationSpecification : BaseSpecification<ProductVariation>
    {
        public ProductVariationSpecification() : base(){}
        public ProductVariationSpecification(long id) : base(x => x.Id == id){}

        public ProductVariationSpecification(long id, bool byProduct) : base(x => x.ProductId == id){}
    }
}