using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace build9ja.core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }

       
    }
}