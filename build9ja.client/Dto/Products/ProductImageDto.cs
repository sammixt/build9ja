using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto.Products 
{
    public class ProductImageDto 
    {
         public string MainImage { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string ImageThree { get; set; }
        public string ImageFour { get; set; }
        public string ImageFive { get; set; }
        public string ImageSix { get; set; }

        [Required(ErrorMessage = "Image Type is required")]
        public string ImageType {get; set;}
       public long ProductId {get; set;}
       public string ProductIdString {get; set;}
    }
}