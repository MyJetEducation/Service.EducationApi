using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Service.UserInfo.Crud.Grpc;
using SimpleTrading.ClientApi.Utils;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/auth/v1")]
	[OpenApiTag("Auth", Description = "user authorization")]
	public class AuthController : BaseController
	{
		private readonly ITokenService _tokenService;

		public AuthController(ITokenService tokenService,
			ILogger<AuthController> logger,
			IUserInfoService userInfoService) : base(userInfoService, logger) => _tokenService = tokenService;

		[AllowAnonymous]
		[HttpPost("login")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (TokenInfo), Description = "Ok")]
		[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
		public async ValueTask<IActionResult> LoginAsync([FromBody] LoginRequest request)
		{
			TokenInfo tokenInfo = await _tokenService.GenerateTokensAsync(request.UserName, HttpContext.GetIp(), request.Password);

			return tokenInfo != null
				? DataResponse<TokenInfo>.Ok(tokenInfo)
				: Unauthorized();
		}

		[AllowAnonymous]
		[HttpPost("refresh-token")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (TokenInfo), Description = "Ok")]
		[SwaggerResponse(HttpStatusCode.Forbidden, null, Description = "Forbidden")]
		public async ValueTask<IActionResult> RefreshTokenAsync([FromBody, Required] string refreshToken)
		{
			TokenInfo info = await _tokenService.RefreshTokensAsync(refreshToken, HttpContext.GetIp());

			return info == null
				? Forbid()
				: DataResponse<TokenInfo>.Ok(info);
		}
	}
}