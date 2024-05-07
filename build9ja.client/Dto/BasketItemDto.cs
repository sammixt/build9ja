using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string ProductIdString {get; set;}

        public long VariationId { get; set; }
        public string VariationName { get; set; }
        public string sku {get; set;}
    }
}