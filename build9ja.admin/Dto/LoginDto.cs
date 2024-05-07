using System.ComponentModel.DataAnnotations;

namespace build9ja.admin.Dto
{
	public class LoginDto 
	{
		public LoginDto()
		{
		}
		public string MemberId { get; set; }

		[Required(ErrorMessage = "Username is Required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		public string Password { get; set; }

		public string Permission { get; set; }
	}

	public class LoggedInDto
    {
        public LoggedInDto()
        {

        }

        public LoggedInDto(string token, string userName, string permission, string fullname)
        {
			Token = token;
			UserName = userName;
			Permission = permission;
			FullName = fullname;
        }
		public string Token { get; set; }
		public string UserName { get; set; }
		public string Permission { get; set; }
		public string FullName { get; set; }
    }
}

