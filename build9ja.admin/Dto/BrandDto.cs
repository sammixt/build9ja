using System.ComponentModel.DataAnnotations;

namespace build9ja.admin.Dto
{
    public class BrandDto : UploadImageDto
    {
        public BrandDto()
        {
            
        }

        public long Id {get; set;}

        [Required(ErrorMessage = "Brand name is required")]
        public string BrandName { get; set; }

        public string BrandLogo { get; set; }

        public bool IsTopBrand { get; set;}
    }
}