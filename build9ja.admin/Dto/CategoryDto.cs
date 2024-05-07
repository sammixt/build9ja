using System.ComponentModel.DataAnnotations;

namespace build9ja.admin.Dto
{
    public class CategoryDto : UploadImageDto 
    {
        public long Id {get; set;}
        public long ParentId {get; set;}

        [Required(ErrorMessage = "Category name is required")]
        public string Name {get; set;}
        [Required(ErrorMessage = "Category Description is required")]
        public string Description {get; set;}
        public bool IsTopCategory { get; set;}

        public string ParentCategory {get; set;}
        public string Status {get; set;}
        public string Image {get; set;}

       
    }

    public class CategoryAndSubDto : CategoryDto
    {
         public List<CategoryListDto> SubCategories {get; set;}
    }

    public class CategoryListDto
    {
        public long Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public string Status {get; set;}
        public string Image {get; set;}

    }

    
}