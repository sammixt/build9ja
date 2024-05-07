using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
        }

        public string BrandName { get; set; }

        public string BrandLogo { get; set; }

        public bool IsTopBrand { get; set;}
    }
}