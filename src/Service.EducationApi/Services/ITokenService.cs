using System.Threading.Tasks;
using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public interface ITokenService
	{
		ValueTask<TokenInfo> GenerateTokensAsync(string userName, string ipAddress, string password = null);

		ValueTask<TokenInfo> RefreshTokensAsync(string currentRefreshToken, string ipAddress);
	}
}