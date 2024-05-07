using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.admin.Dto
{
    public class DeliveryMethodDto
    {

        public long Id { get; set; }

		public DateTime DateCreated {get; set;}

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        
        [Required(ErrorMessage = "LGA is required")]
        public string LocalGovt { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public string ShippingId {get; set;}
    }
}