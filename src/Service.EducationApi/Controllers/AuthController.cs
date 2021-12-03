using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public AuthController(ITokenService tokenService) => _tokenService = tokenService;

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async ValueTask<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (request.IsInvalid)
				return StatusResponse.Error(ResponseCode.NoRequestData);

			TokenInfo info = await _tokenService.GenerateTokensAsync(request);

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

			TokenInfo info = await _tokenService.RefreshTokensAsync(refreshToken);

			return info == null
				? Forbid()
				: DataResponse<TokenInfo>.Ok(info);
		}
	}
}