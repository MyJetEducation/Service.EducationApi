using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.EducationApi.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;

namespace Service.EducationApi.Controllers
{
	[ApiController]
	[EnableCors("CorsApi")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public abstract class BaseController : ControllerBase
	{
		protected readonly IUserInfoService UserInfoService;
		protected readonly ILogger Logger;

		protected BaseController(IUserInfoService userInfoService, ILogger logger)
		{
			UserInfoService = userInfoService;
			Logger = logger;
		}

		protected static void WaitFakeRequest() => Thread.Sleep(200);

		protected static IActionResult Result(bool? isSuccess) => isSuccess == true ? StatusResponse.Ok() : StatusResponse.Error();

		protected async ValueTask<Guid?> GetUserIdAsync(string userName = null)
		{
			string identityName = userName ?? User.Identity?.Name;

			UserInfoResponse userInfoResponse = await UserInfoService.GetUserInfoByLoginAsync(new UserInfoAuthRequest
			{
				UserName = identityName
			});

			return userInfoResponse?.UserInfo?.UserId;
		}

		protected string GetIpAddress()
		{
			string requestHeader = Request.Headers.ContainsKey("X-Forwarded-For")
				? (string) Request.Headers["X-Forwarded-For"]
				: HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

			if (requestHeader == null)
				throw new Exception("Can't obtain user IP address. Skip request");

			Logger.LogDebug("User IP is: {ip}", requestHeader);

			return requestHeader;
		}
	}
}