using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public class TokenService : ITokenService
	{
		private static readonly UserInfo[] UserInfos;

		/// <summary>
		///     Todo: remove after use db
		/// </summary>
		static TokenService()
		{
			UserInfos = new[]
			{
				new UserInfo
				{
					UserName = "user",
					Password = "123",
					Role = UserRole.Default
				}
			};
		}

		public LoginResponse GenerateTokens(LoginRequest request)
		{
			UserInfo userInfo = UserInfos.FirstOrDefault(info => info.UserName == request.UserName && info.Password == request.Password);

			return userInfo != null
				? UpdateTokens(userInfo)
				: null;
		}

		public LoginResponse RefreshTokens(string currentRefreshToken)
		{
			if (string.IsNullOrWhiteSpace(currentRefreshToken))
				return null;

			UserInfo userInfo = UserInfos.FirstOrDefault(info => info.RefreshToken == currentRefreshToken);

			return userInfo?.IsRefreshTokenActive == true
				? UpdateTokens(userInfo)
				: null;
		}

		private static LoginResponse UpdateTokens(UserInfo userInfo)
		{
			SetJwtToken(userInfo);
			SetRefreshToken(userInfo);

			return new LoginResponse(userInfo);
		}

		private static void SetJwtToken(UserInfo userInfo)
		{
			byte[] key = Encoding.ASCII.GetBytes(Program.JwtSecret);
			string clientId = userInfo.UserName;

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Aud, "education-api"),
				new Claim(ClaimsIdentity.DefaultNameClaimType, clientId),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, userInfo.Role)
			};

			var identity = new GenericIdentity(clientId);
			int expireMinutes = Program.Settings.JwtTokenExpireMinutes;

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(identity, claims),
				Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			userInfo.JwtToken = tokenHandler.WriteToken(token);
		}

		private static void SetRefreshToken(UserInfo userInfo)
		{
			byte[] key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

			userInfo.RefreshToken = Convert.ToBase64String(key);

			int expireMinutes = Program.Settings.RefreshTokenExpireMinutes;

			userInfo.RefreshTokenExpires = DateTime.UtcNow.AddMinutes(expireMinutes);
		}
	}
}