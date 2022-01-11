using System;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using build9ja.core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace build9ja.infrastructure.Services
{
	public class TokenService :  ITokenService
	{
		public TokenService()
		{
		}

        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(string StaffId, string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, StaffId),
                new Claim(ClaimTypes.GivenName, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha384Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

