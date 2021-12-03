using System.Threading.Tasks;
using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public interface ITokenService
	{
		ValueTask<TokenInfo> GenerateTokensAsync(LoginRequest request, string ipAddress);
		ValueTask<TokenInfo> RefreshTokensAsync(string currentRefreshToken, string ipAddress);
	}
}