using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.client.Dto.Products;

namespace build9ja.client.Dto
{
    public class Pagination
    {
        public Pagination(){}
        public Pagination(int pageIndex, int pageSize, int count, List<ProductDto> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<ProductDto> Data { get; set; }
    }

    public class PaginationWishlist
    {
        public PaginationWishlist(){}
        public PaginationWishlist(int pageIndex, int pageSize, int count, List<WishListDto> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<WishListDto> Data { get; set; }
    }
}