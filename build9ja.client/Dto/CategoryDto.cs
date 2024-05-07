using System.ComponentModel.DataAnnotations;

namespace build9ja.client.Dto
{
    public class CategoryDto  
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

    public class HeaderCategoryDto
    {
        public HeaderCategoryDto()
        {
            CategoryOne = new List<CategoryAndSubDto>();
            CategoryTwo = new List<CategoryAndSubDto>();
            CategoryThree = new List<CategoryAndSubDto>();
            CategoryFour = new List<CategoryAndSubDto>();
        }
        public List<CategoryAndSubDto> CategoryOne { get; set; }
        public List<CategoryAndSubDto> CategoryTwo { get; set; }
        public List<CategoryAndSubDto> CategoryThree { get; set; }
        public List<CategoryAndSubDto> CategoryFour { get; set; }
    }

    
}