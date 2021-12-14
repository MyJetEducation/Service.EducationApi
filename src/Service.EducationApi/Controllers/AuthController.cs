using System;
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
		private readonly ILoginRequestValidator _loginRequestValidator;

		public AuthController(ITokenService tokenService, 
			ILogger<AuthController> logger, 
			ILoginRequestValidator loginRequestValidator, 
			IUserInfoService userInfoService) : base(userInfoService)
		{
			_tokenService = tokenService;
			_logger = logger;
			_loginRequestValidator = loginRequestValidator;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (_loginRequestValidator.ValidateRequired(request))
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			TokenInfo info = await _tokenService.GenerateTokensAsync(request, GetIpAddress());

			return info == null
				? Unauthorized()
				: DataResponse<TokenInfo>.Ok(info);
		}

		[AllowAnonymous]
		[HttpPost("refresh-token")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async ValueTask<IActionResult> RefreshToken([FromBody, Required] string refreshToken)
		{
			if (refreshToken.IsNullOrWhiteSpace())
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			_logger.LogDebug("RefreshToken is: {token}", refreshToken);

			TokenInfo info = await _tokenService.RefreshTokensAsync(refreshToken, GetIpAddress());

			return info == null
				? Forbid()
				: DataResponse<TokenInfo>.Ok(info);
		}

		private string GetIpAddress()
		{
			string requestHeader = Request.Headers.ContainsKey("X-Forwarded-For")
				? (string) Request.Headers["X-Forwarded-For"]
				: HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

			if (requestHeader == null)
				throw new Exception("Can't obtain user IP address. Skip request");

			_logger.LogDebug("User IP is: {ip}", requestHeader);

			return requestHeader;
		}
	}
}