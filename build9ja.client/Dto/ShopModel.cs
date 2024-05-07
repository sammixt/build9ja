using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.client.Dto.Products;

namespace build9ja.client.Dto
{
    public class ShopModel
    {
        public Pagination PaginatedProducts {get; set;}
        public PaginationWishlist PaginatedWishlist {get; set;}
    }
}