using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(){}
        public CategorySpecification(long id) : base(x => x.Id == id){}

        public CategorySpecification(bool topCategory) : base(x => x.IsTopCategory == topCategory){}
        public CategorySpecification(long parentId, bool byParent) : base(x => (byParent) ? x.Id == parentId : x.ParentId == parentId){}
        public CategorySpecification(string name) : base(x => x.Name.Trim().ToLower().Equals(name.Trim().ToLower())){}

        public CategorySpecification(DataTableRequestSpecification spec)
		: base(x => (string.IsNullOrEmpty(spec.SearchValue) || x.Name.Contains(spec.SearchValue)
		|| x.Description.Contains(spec.SearchValue))){
			ApplyPaging(((spec.Skip / spec.PageSize) * spec.PageSize),spec.PageSize);
		}
    }
}