using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class BrandSpecification : BaseSpecification<Brand>
    {
        public BrandSpecification()
        {
        }

        public BrandSpecification(long id) : base(x => x.Id == id)
        {

        }

        public BrandSpecification(string name) : base(x => x.BrandName.ToLower() == name)
        {

        }

        public BrandSpecification(DataTableRequestSpecification spec)
		: base(x => (string.IsNullOrEmpty(spec.SearchValue) || x.BrandName.Contains(spec.SearchValue))){
			ApplyPaging(((spec.Skip / spec.PageSize) * spec.PageSize),spec.PageSize);
		}

    }
}