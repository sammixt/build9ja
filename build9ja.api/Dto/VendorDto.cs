using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.api.Dto
{
    public class VendorDto
    {
        public long Id {get; set;}
        public string SellerId {get; set;}
        [Required(ErrorMessage = "Company or Business name is required")]
        [MaxLength(350,ErrorMessage = "Company or Business name should not exceed 350 characters")]
        public string Company {get; set;}
        public string TaxNumber {get; set;}
        public string CacNumber {get; set;}
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(100,ErrorMessage = "First Name should not exceed 100 characters")]
        public string FirstName {get; set;}
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100,ErrorMessage = "Last Name should not exceed 100 characters")]
        public string LastName {get; set;}
        [Required(ErrorMessage ="Email address is required")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
        public string Email {get; set;}
        [Required(ErrorMessage = "Phone number is required")]
		[RegularExpression("^\\d{7,15}$",ErrorMessage = "Phone number should be between 7 to 15 digits")]
        public string PhoneNumber {get; set;}
        [MaxLength(500,ErrorMessage = "Description should not exceed 500 characters")]
        public string Description {get; set;}
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(500,ErrorMessage = "Address should not exceed 500 characters")]
        public string Address {get; set;}
        public string City {get; set;}
        [Required(ErrorMessage = "Address is required")]
         [MaxLength(50,ErrorMessage = "State should not exceed 50 characters")]
        public string State {get; set;}
        public string Status {get; set;}
    }

    public class StatusDto{
        [Required(ErrorMessage = "Status is required")]
        public string Status {get; set;}
    }
    
}