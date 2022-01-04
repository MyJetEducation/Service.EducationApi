using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;

namespace Service.EducationApi.Controllers
{
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public abstract class BaseController : ControllerBase
	{
		protected readonly IUserInfoService UserInfoService;

		protected BaseController(IUserInfoService userInfoService) => UserInfoService = userInfoService;

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
	}
}