using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.EducationApi.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Contracts;
using Service.UserInfo.Crud.Grpc.Models;

namespace Service.EducationApi.Services
{
	public class TokenService : ITokenService
	{
		private readonly IUserInfoService _userInfoService;
		private readonly string _jwtSecret;
		private readonly int _jwtTokenExpireMinutes;
		private readonly int _refreshTokenExpireMinutes;
		private readonly ILogger<TokenService> _logger;

		public TokenService(IUserInfoService userInfoService, string jwtSecret, int jwtTokenExpireMinutes, int refreshTokenExpireMinutes, ILogger<TokenService> logger)
		{
			_userInfoService = userInfoService;
			_jwtSecret = jwtSecret;
			_jwtTokenExpireMinutes = jwtTokenExpireMinutes;
			_refreshTokenExpireMinutes = refreshTokenExpireMinutes;
			_logger = logger;
		}

		public async ValueTask<TokenInfo> GenerateTokensAsync(LoginRequest request, string ipAddress)
		{
			UserAuthInfoResponse userInfo = await _userInfoService.GetUserInfoByLoginAsync(new UserInfoLoginRequest {UserName = request.UserName });
			UserAuthInfoGrpcModel authInfo = userInfo?.UserAuthInfo;

			_logger.LogDebug("Answer for GetUserInfoByLoginAsync: {answer}", JsonSerializer.Serialize(userInfo));

			return authInfo != null && authInfo.Password == request.Password
				? await GetNewTokenInfo(authInfo, ipAddress)
				: await ValueTask.FromResult<TokenInfo>(null);
		}

		public async ValueTask<TokenInfo> RefreshTokensAsync(string currentRefreshToken, string ipAddress)
		{
			UserAuthInfoResponse userInfo = await _userInfoService.GetUserInfoByTokenAsync(new UserInfoTokenRequest {RefreshToken = currentRefreshToken });
			UserAuthInfoGrpcModel authInfo = userInfo?.UserAuthInfo;

			_logger.LogDebug("Answer for GetUserInfoByTokenAsync: {answer}", JsonSerializer.Serialize(userInfo));

			return authInfo != null && authInfo.IpAddress == ipAddress && DateTime.UtcNow < authInfo.RefreshTokenExpires
				? await GetNewTokenInfo(authInfo, ipAddress)
				: await ValueTask.FromResult<TokenInfo>(null);
		}

		private async ValueTask<TokenInfo> GetNewTokenInfo(UserAuthInfoGrpcModel userAuthInfo, string ipAddress)
		{
			var newTokenInfoRequest = new UserNewTokenInfoRequest
			{
				IpAddress = ipAddress,
				UserId = userAuthInfo.UserId
			};

			SetJwtToken(newTokenInfoRequest, userAuthInfo);
			SetRefreshToken(newTokenInfoRequest);

			_logger.LogDebug("UserNewTokenInfoRequest for UpdateUserTokenInfoAsync: {answer}", JsonSerializer.Serialize(newTokenInfoRequest));

			CommonResponse response = await _userInfoService.UpdateUserTokenInfoAsync(newTokenInfoRequest);

			_logger.LogDebug("Answer for UpdateUserTokenInfoAsync: {answer}", JsonSerializer.Serialize(response));

			if (!response.IsSuccess)
				return await ValueTask.FromResult<TokenInfo>(null);

			return new TokenInfo(newTokenInfoRequest);
		}

		private void SetJwtToken(UserNewTokenInfoRequest tokenInfoRequest, UserAuthInfoGrpcModel userAuthInfo)
		{
			byte[] key = Encoding.ASCII.GetBytes(_jwtSecret);
			string clientId = userAuthInfo.UserName;

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Aud, Program.Settings.JwtAudience),
				new Claim(ClaimsIdentity.DefaultNameClaimType, clientId),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, userAuthInfo.Role)
			};

			var identity = new GenericIdentity(clientId);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(identity, claims),
				Expires = DateTime.UtcNow.AddMinutes(_jwtTokenExpireMinutes),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			tokenInfoRequest.JwtToken = tokenHandler.WriteToken(token);
		}

		private void SetRefreshToken(UserNewTokenInfoRequest tokenInfoRequest)
		{
			byte[] key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

			tokenInfoRequest.RefreshToken = Convert.ToBase64String(key);

			tokenInfoRequest.RefreshTokenExpires = DateTime.UtcNow.AddMinutes(_refreshTokenExpireMinutes);
		}
	}
}