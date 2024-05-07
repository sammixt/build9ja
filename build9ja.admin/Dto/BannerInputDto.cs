using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.admin.Dto
{
    public class BannerInputDto : UploadImageDto
    {
        public BannerInputDto()
        {
        }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }
        public SliderTypes SliderTypes { get; set; }
    }

    public enum SliderTypes
    {
        BannerOne = 1,
        BannerTwo = 2,
        BannerThree = 3,
        BannerFour = 4,
        SubPageBanner = 5
    }
}