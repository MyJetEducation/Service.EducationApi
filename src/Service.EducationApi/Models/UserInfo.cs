using System;

namespace Service.EducationApi.Models
{
	public class UserInfo
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public string JwtToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime? RefreshTokenExpires { get; set; }

		public bool IsRefreshTokenActive => RefreshTokenExpires != null && RefreshTokenExpires > DateTime.UtcNow;
	}
}