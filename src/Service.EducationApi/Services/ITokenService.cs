using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public interface ITokenService
	{
		LoginResponse GenerateTokens(LoginRequest request);
		LoginResponse RefreshTokens(string currentRefreshToken);
	}
}