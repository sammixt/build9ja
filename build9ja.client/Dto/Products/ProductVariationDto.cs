using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto.Products
{
    public class ProductVariationDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Variation is Required")]
        public string Variation { get; set; }
        [Required(ErrorMessage = "SKU is Required")]
        public string SKU { get; set; }
        public string IMEI { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1,1000000)]
        public int Quantity { get; set; }
        [Required(ErrorMessage ="Selling Price is required")]
        [Range(1,100000000)]
        public decimal SellingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public Nullable<DateTime> SaleStartDate {get; set; }
        public Nullable<DateTime> SaleEndDate {get; set; }

        public Nullable<long> ProductId { get; set; }
       // public virtual ProductDto Product { get; set; }

        public string ProductIdString {get; set;}
    }
}