using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public AuthController(ITokenService tokenService) => _tokenService = tokenService;

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
		{
			LoginResponse response = _tokenService.GenerateTokens(request);

			return GetLoginResultResponse(response);
		}

		[AllowAnonymous]
		[HttpPost("refresh-token")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public ActionResult<LoginResponse> RefreshToken([FromBody, SwaggerRequestBody(Required = true)] string refreshToken)
		{
			LoginResponse response = _tokenService.RefreshTokens(refreshToken);

			return GetLoginResultResponse(response);
		}

		[HttpGet("who")]
		[Authorize]
		public ActionResult<WhoResponse> Who() => Response<WhoResponse>.Result(new WhoResponse
		{
			UserName = User.Identity?.Name
		});

		private ActionResult<LoginResponse> GetLoginResultResponse(LoginResponse response) => response == null
			? Unauthorized()
			: Response<LoginResponse>.Result(response);
	}
}