using Service.UserInfo.Crud.Grpc.Contracts;

namespace Service.EducationApi.Models
{
	public class TokenInfo
	{
		public TokenInfo(UserNewTokenInfoRequest tokenInfoRequest)
		{
			Token = tokenInfoRequest.JwtToken;
			RefreshToken = tokenInfoRequest.RefreshToken;
		}

		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}