using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class WishList : BaseEntity
    {
        public WishList()
        {
        }

        public string user { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}