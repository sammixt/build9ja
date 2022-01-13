using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.api.Dto
{
    public class CategoryDto
    {
        public long Id {get; set;}
        public long ParentId {get; set;}

        [Required(ErrorMessage = "Category name is required")]
        public string Name {get; set;}
        [Required(ErrorMessage = "Category Description is required")]
        public string Description {get; set;}

        public string ParentCategory {get; set;}
        public string Status {get; set;}
        public string Image {get; set;}

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