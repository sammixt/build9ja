using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class ProductImage : BaseEntity
    {
        public string MainImage { get; set; }
        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string ImageThree { get; set; }
        public string ImageFour { get; set; }
        public string ImageFive { get; set; }
        public string ImageSix { get; set; }

        public virtual Product Product {get; set;}
        public long ProductId {get; set;}
    }
}