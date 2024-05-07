using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class HomePageModel
    {
        public HomePageModel()
        {
            Banner = new BannerDto();
            Category = new List<CategoryAndSubDto>();
            
        }
        public BannerDto Banner {get; set;}
        public List<CategoryAndSubDto> Category {get; set;}
        public CategoryAndSubDto TopCategoryA {get; set;}
        public CategoryAndSubDto TopCategoryB {get; set;}
        public CategoryAndSubDto TopCategoryC {get; set;}
        public CategoryAndSubDto TopCategoryD {get; set;}
    }
}