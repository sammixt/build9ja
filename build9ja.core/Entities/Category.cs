using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            
        }
        public long ParentId {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public string Status {get; set;}
        public string Image {get; set;}
        public bool IsTopCategory { get; set;}

        public string CreatedBy {get; set;}
    }
}