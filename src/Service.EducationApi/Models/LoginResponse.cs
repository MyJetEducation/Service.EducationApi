namespace Service.EducationApi.Models
{
	public class LoginResponse
	{
		public LoginResponse(UserInfo userInfo)
		{
			Token = userInfo.JwtToken;
			RefreshToken = userInfo.RefreshToken;
		}

		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}