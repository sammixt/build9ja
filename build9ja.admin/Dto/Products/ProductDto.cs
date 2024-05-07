using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using build9ja.admin.enums;

namespace build9ja.admin.Dto.Products
{
    public class ProductDto
    {
        public string ProductIdString {get; set;}

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }
       
        public string ProductDescription { get; set; }

        public string YoutubeLink {get; set;}

        public string KeyFeatures {get; set;}

        public string BoxContent {get; set;}

        public string AdditionalNote {get; set;}
       
        public string Vendor {get; set;}
        public long VendorId {get; set;}
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public string Brand { get; set; }
        public long BrandId { get; set; }
        public bool IsActive {get; set;}
        public bool IsFeatured {get; set;}

        [Required(ErrorMessage = "Base price is required")]
        public decimal BasePrice {get; set;}

        public string Status {get; set;}

        //public long ProductImageId {get; set;}
        public virtual ProductImageDto ProductImage {get; set;}

        public virtual ProductSpecificationDto ProductSpecification {get; set;}
       // public long ProductSpecificationId { get; set; }
        public List<ProductVariationDto> ProductVariations {get; set;}

        public DateTime DateCreated {get; set;}
    }

    public class ProductStatusDto {
        public string ProductIdString {get; set;}
        public string Status {get; set;}

        public bool IsFeatured {get; set;}
    }
}