using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.Core.Grpc.Models;
using Service.EducationApi.Constants;
using Service.EducationApi.Mappers;
using Service.EducationApi.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("api/useraccount/v1")]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[OpenApiTag("UserAccount", Description = "user account")]
	public class UserAccountController : BaseController
	{
		private readonly IUserProfileService _userProfileService;

		public UserAccountController(IUserInfoService userInfoService,
			IUserProfileService userProfileService,
			ILogger<UserAccountController> logger)
			: base(userInfoService, logger) => _userProfileService = userProfileService;

		[HttpPost("get")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<UserAccount>), Description = "Ok")]
		public async ValueTask<IActionResult> GetAccountAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			AccountGrpcResponse account = await _userProfileService.GetAccount(new GetAccountGrpcRequest {UserId = userId});

			AccountDataGrpcModel accountData = account?.Data;

			return accountData == null
				? StatusResponse.Error(ResponseCode.NoResponseData)
				: DataResponse<UserAccount>.Ok(accountData.ToModel());
		}

		[HttpPost("put")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (StatusResponse), Description = "Status")]
		public async ValueTask<IActionResult> SaveAccountAsync([FromBody] UserAccount account)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _userProfileService.SaveAccount(account.ToGrpcModel(userId));

			return Result(response?.IsSuccess);
		}
	}
}