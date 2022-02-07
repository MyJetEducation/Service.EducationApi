using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.Core.Client.Constants;
using Service.Core.Client.Education;
using Service.Core.Client.Services;
using Service.EducationPersonalApi.Models;
using Service.TimeLogger.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;

namespace Service.EducationPersonalApi.Controllers
{
	[Authorize]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	public class BaseController : ControllerBase
	{
		private readonly ISystemClock _systemClock;
		private readonly IEncoderDecoder _encoderDecoder;
		private readonly IUserInfoService _userInfoService;
		private readonly ILogger _logger;

		public BaseController(ISystemClock systemClock,
			IEncoderDecoder encoderDecoder,
			IUserInfoService userInfoService,
			ILogger<EducationController> logger)
		{
			_systemClock = systemClock;
			_encoderDecoder = encoderDecoder;
			_userInfoService = userInfoService;
			_logger = logger;
		}

		protected async ValueTask<IActionResult> Process<TGrpcResponse, TModelResponse>(
			Func<Guid?, ValueTask<TGrpcResponse>> grpcRequestFunc,
			Func<TGrpcResponse, TModelResponse> responseFunc)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			TGrpcResponse response = await grpcRequestFunc.Invoke(userId);

			return DataResponse<TModelResponse>.Ok(responseFunc.Invoke(response));
		}

		protected async ValueTask<IActionResult> ProcessTask<TGrpcResponse, TModelResponse>(
			int unit, int task, TaskRequestBase request,
			Func<Guid?, TimeSpan, ValueTask<TGrpcResponse>> grpcRequestFunc,
			Func<TGrpcResponse, TModelResponse> responseFunc)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			TimeSpan? duration = GetTimeTokenDuration(request.TimeToken, userId, unit, task);
			if (duration == null)
				return StatusResponse.Error(ResponseCode.InvalidTimeToken);

			TGrpcResponse response = await grpcRequestFunc.Invoke(userId, duration.Value);

			return DataResponse<TModelResponse>.Ok(responseFunc.Invoke(response));
		}

		private TimeSpan? GetTimeTokenDuration(string timeToken, Guid? userId, int unit, int task)
		{
			TaskTimeLogGrpcRequest tokenData;

			try
			{
				tokenData = _encoderDecoder.DecodeProto<TaskTimeLogGrpcRequest>(timeToken);
			}
			catch (Exception exception)
			{
				_logger.LogError("Can't decode time token ({token}) for user {user}, with message {message}", timeToken, userId, exception.Message);
				return null;
			}

			if (tokenData.Tutorial != EducationTutorial.PersonalFinance
				|| tokenData.Unit != unit
				|| tokenData.Task != task)
				return null;

			TimeSpan span = _systemClock.Now.Subtract(tokenData.StartDate);

			return span == TimeSpan.Zero ? (TimeSpan?) null : span;
		}

		protected async ValueTask<Guid?> GetUserIdAsync()
		{
			UserInfoResponse userInfoResponse = await _userInfoService.GetUserInfoByLoginAsync(new UserInfoAuthRequest
			{
				UserName = User.Identity?.Name
			});

			return userInfoResponse?.UserInfo?.UserId;
		}
	}
}