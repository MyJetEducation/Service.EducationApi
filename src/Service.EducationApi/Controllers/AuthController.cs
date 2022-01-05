using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Core.Domain.Extensions;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/auth/v1")]
	public class AuthController : BaseController
	{
		private readonly ITokenService _tokenService;
		private readonly ILogger<AuthController> _logger;

		public AuthController(ITokenService tokenService,
			ILogger<AuthController> logger,
			IUserInfoService userInfoService) : base(userInfoService, logger)
		{
			_tokenService = tokenService;
			_logger = logger;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async ValueTask<IActionResult> LoginAsync([FromBody] LoginRequest request)
		{
			TokenInfo tokenInfo = await _tokenService.GenerateTokensAsync(request.UserName, GetIpAddress(), request.Password);

			return tokenInfo != null
				? DataResponse<TokenInfo>.Ok(tokenInfo)
				: Unauthorized();
		}

		[AllowAnonymous]
		[HttpPost("refresh-token")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async ValueTask<IActionResult> RefreshTokenAsync([FromBody, Required] string refreshToken)
		{
			if (refreshToken.IsNullOrWhiteSpace())
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			_logger.LogDebug("RefreshTokenAsync is: {token}", refreshToken);

			TokenInfo info = await _tokenService.RefreshTokensAsync(refreshToken, GetIpAddress());

			return info == null
				? Forbid()
				: DataResponse<TokenInfo>.Ok(info);
		}
	}
}