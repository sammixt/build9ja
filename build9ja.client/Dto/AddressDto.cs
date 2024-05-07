using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class AddressDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country {get; set;}

    }
}