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
        public CategorySpecification(long parentId, bool byParent) : base(x => (byParent) ? x.Id == parentId : x.ParentId == parentId){}
        public CategorySpecification(string name) : base(x => x.Name.Trim().ToLower().Equals(name.Trim().ToLower())){}

    }
}