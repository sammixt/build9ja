using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto.Products
{
    public class ProductExtDto : ProductDto
    {
        public int totalStock  {get
        {
            return ProductVariations.Sum(n => n.Quantity);
        }}
    }
}