using System;
namespace build9ja.core.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(string StaffId, string userName, string role);
	}
}

