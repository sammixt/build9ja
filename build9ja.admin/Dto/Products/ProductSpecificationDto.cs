using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.admin.Dto.Products
{
    public class ProductSpecificationDto
    {
        public string Dimension {get; set;}
        public int Weight {get; set;}
        public string Model {get; set;}
        public string ProductType {get; set;}
        public string MainMaterial {get; set;}
        public string ColorFamily {get; set;}

        public long ProductId {get; set;}

        public string ProductIdString {get; set;}
    }
}