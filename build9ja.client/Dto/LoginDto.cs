using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class LoginDto
    {
        public LoginDto()
        {
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ReturnUrl {get; set;}
    }
}