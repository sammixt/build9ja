using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
        }

        public string ProductIdString {get; set;}
        public string ProductName { get; set; }
       
        public string ProductDescription { get; set; }

        public string YoutubeLink {get; set;}

        public string KeyFeatures {get; set;}

        public string BoxContent {get; set;}

        public string AdditionalNote {get; set;}
       
        public Vendor Vendor {get; set;}
        public long VendorId {get; set;}
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        public Brand Brand { get; set; }
        public Nullable<long> BrandId { get; set; }
        public bool IsActive {get; set;}

        public bool IsFeatured {get; set;}

        public decimal BasePrice {get; set;}

        public string Status {get; set;}

        //public long ProductImageId {get; set;}
        public virtual ProductImage ProductImage {get; set;}

        public virtual ProductSpecification ProductSpecification {get; set;}
       // public long ProductSpecificationId { get; set; }
        public List<ProductVariation> ProductVariations {get; set;}

    }
}