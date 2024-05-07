using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using productEntities = build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class ProductSpecificationsSpecification : BaseSpecification<productEntities.ProductSpecification>
    {
        public ProductSpecificationsSpecification() : base(){}
        public ProductSpecificationsSpecification(long id) : base(x => x.Id == id){}

        public ProductSpecificationsSpecification(long id, bool byProduct) : base(x => x.ProductId == id){}
    }
}