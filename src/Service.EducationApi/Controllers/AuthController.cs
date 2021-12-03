using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/auth/v1")]
	public class AuthController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly ILogger<AuthController> _logger;

		public AuthController(ITokenService tokenService, ILogger<AuthController> logger)
		{
			_tokenService = tokenService;
			_logger = logger;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async ValueTask<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
				return StatusResponse.Error(ResponseCode.NoRequestData);

			TokenInfo info = await _tokenService.GenerateTokensAsync(request, GetIpAddress());

			return info == null
				? Unauthorized()
				: DataResponse<TokenInfo>.Ok(info);
		}

		[AllowAnonymous]
		[HttpPost("refresh-token")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async ValueTask<IActionResult> RefreshToken([FromBody, SwaggerRequestBody(Required = true)] string refreshToken)
		{
			if (string.IsNullOrWhiteSpace(refreshToken))
				return StatusResponse.Error(ResponseCode.NoRequestData);

			_logger.LogDebug("RefreshToken is: {token}", refreshToken);

			TokenInfo info = await _tokenService.RefreshTokensAsync(refreshToken, GetIpAddress());

			_logger.LogDebug("Answer for RefreshTokensAsync: {answer}", info);

			return info == null
				? Forbid()
				: DataResponse<TokenInfo>.Ok(info);
		}

		private string GetIpAddress()
		{
			string requestHeader = Request.Headers.ContainsKey("X-Forwarded-For") 
				? (string) Request.Headers["X-Forwarded-For"] 
				: HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

			_logger.LogDebug("User IP is: {ip}", requestHeader);

			return requestHeader;
		}
	}
}