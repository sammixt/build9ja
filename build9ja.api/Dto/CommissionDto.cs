using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.api.Dto
{
    public class CommissionDto
    {
        public long Id {get; set;}

        [Required (ErrorMessage="Commission name is required")]
        public string CommissionType {get; set;}
        
        [Required(ErrorMessage = "Commission percent is required")]  
        [Range(1, 100, ErrorMessage = "Commission percent must be between 1 and 100")]
        public decimal CommissionPercentage {get; set;}
        public string Status {get; set;}
    }
}