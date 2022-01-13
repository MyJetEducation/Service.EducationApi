using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.Core.Grpc.Models;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.EducationRetry.Grpc;
using Service.EducationRetry.Grpc.Models;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/retry/v1")]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[OpenApiTag("Retry", Description = "Task retry")]
	public class TaskRetryController : BaseController
	{
		private readonly IEducationRetryService _educationRetryService;

		public TaskRetryController(IUserInfoService userInfoService, ILogger logger, IEducationRetryService educationRetryService)
			: base(userInfoService, logger) => _educationRetryService = educationRetryService;

		[HttpPost("count")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<int>), Description = "Ok")]
		public async ValueTask<IActionResult> GetRetryCountAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			RetryCountGrpcResponse response = await _educationRetryService.GetRetryCountAsync(new GetRetryCountGrpcRequest
			{
				UserId = userId
			});

			return DataResponse<int>.Ok(response.Count);
		}

		[HttpPost("use/date")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<int>), Description = "Ok")]
		public async ValueTask<IActionResult> UseRetryByDateAsync([FromBody] UseRetryRequest request)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _educationRetryService.DecreaseRetryDateAsync(new DecreaseRetryDateGrpcRequest
			{
				UserId = userId,
				Tutorial = request.Tutorial,
				Unit = request.Unit,
				Task = request.Task
			});

			return Result(response?.IsSuccess);
		}

		[HttpPost("use/count")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<int>), Description = "Ok")]
		public async ValueTask<IActionResult> UseRetryByCountAsync([FromBody] UseRetryRequest request)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _educationRetryService.DecreaseRetryCountAsync(new DecreaseRetryCountGrpcRequest
			{
				UserId = userId,
				Tutorial = request.Tutorial,
				Unit = request.Unit,
				Task = request.Task
			});

			return Result(response?.IsSuccess);
		}
	}
}