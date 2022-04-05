using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Authorization.Http;
using NSwag.Annotations;
using Service.Core.Client.Extensions;
using Service.Core.Client.Services;
using Service.Education.Structure;
using Service.TimeLogger.Grpc.Models;
using Service.WalletApi.EducationPersonalApi.Controllers.Contracts;
using Service.Web;

namespace Service.WalletApi.EducationPersonalApi.Controllers
{
	[Authorize]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	public class BaseController : ControllerBase
	{
		public static readonly EducationTutorial Tutorial = EducationTutorial.PersonalFinance;

		private readonly ISystemClock _systemClock;
		private readonly IEncoderDecoder _encoderDecoder;
		private readonly ILogger _logger;

		public BaseController(ISystemClock systemClock, IEncoderDecoder encoderDecoder, ILogger<EducationController> logger)
		{
			_systemClock = systemClock;
			_encoderDecoder = encoderDecoder;
			_logger = logger;
		}

		protected async ValueTask<IActionResult> Process<TGrpcResponse, TModelResponse>(
			Func<Guid?, ValueTask<TGrpcResponse>> grpcRequestFunc,
			Func<TGrpcResponse, TModelResponse> responseFunc)
		{
			Guid? userId = GetUserId();
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
			Guid? userId = GetUserId();
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

			if (tokenData.Tutorial != Tutorial || tokenData.Unit != unit || tokenData.Task != task)
				return null;

			TimeSpan span = _systemClock.Now.Subtract(tokenData.StartDate);

			return span == TimeSpan.Zero ? (TimeSpan?) null : span;
		}

		protected Guid? GetUserId()
		{
			string clientId = this.GetClientId();
			if (clientId.IsNullOrWhiteSpace())
				return null;

			return Guid.TryParse(clientId, out Guid uid) ? (Guid?)uid : null;
		}
	}
}