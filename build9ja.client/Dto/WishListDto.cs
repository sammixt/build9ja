using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.client.Dto.Products;

namespace build9ja.client.Dto
{
    public class WishListDto
    {
        public WishListDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string user { get; set; }

        public long ProductId { get; set; }
        public string ProductIdString { get; set; }
        public ProductExtDto Product { get; set; }
    }
}