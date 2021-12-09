using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
	public class UserAccountController : BaseController
	{
		private readonly IUserProfileService _userProfileService;

		public UserAccountController(IUserInfoService userInfoService, IUserProfileService userProfileService) : base(userInfoService) =>
			_userProfileService = userProfileService;

		[HttpPost("get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> GetAccount()
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
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> SaveAccount([FromBody] UserAccount account)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _userProfileService.SaveAccount(account.ToGrpcModel(userId));

			return Result(response?.IsSuccess);
		}
	}
}