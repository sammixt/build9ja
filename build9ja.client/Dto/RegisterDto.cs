using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class RegisterDto
    {
         public RegisterDto()
        {
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //public string UserName { get; set; }
        /// <summary>
        /// password must contain 1 uppercase letters
        /// password must contain 1 lowercase letters
        /// password must contain 1 non-alpha numeric number
        /// password is 8-16 characters with no space
        /// </summary>
        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,16}$",ErrorMessage = "Password must be 8-16 char " +
            "long 1 uppercase, 1 lowercase, 1 non-alphanumeric number")]
        public string Password { get; set; }

        public string ReturnUrl {get; set;}
    }
}