using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Service.Core.Client.Constants;
using Service.Core.Client.Education;
using Service.Core.Client.Models;
using Service.EducationApi.Mappers;
using Service.EducationApi.Models;
using Service.TutorialPersonal.Grpc;
using Service.TutorialPersonal.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;
using Service.UserReward.Grpc;
using Service.UserReward.Grpc.Models;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[Route("/api/v1/education/personal")]
	public class EducationPersonalController : ControllerBase
	{
		private readonly ITutorialPersonalService _tutorialService;
		private readonly IUserInfoService _userInfoService;
		private readonly IUserRewardService _userRewardService;

		public EducationPersonalController(ITutorialPersonalService tutorialService, IUserInfoService userInfoService, IUserRewardService userRewardService)
		{
			_tutorialService = tutorialService;
			_userInfoService = userInfoService;
			_userRewardService = userRewardService;
		}

		[HttpPost("started")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<PersonalStateResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> LearningStartedAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _userRewardService.LearningStartedAsync(new LearningStartedGrpcRequest
			{
				UserId = userId,
				Tutorial = EducationTutorial.PersonalFinance,
				Unit = 1,
				Task = 1
			});

			return StatusResponse.Result(response.IsSuccess);
		}

		[HttpPost("dashboard")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<PersonalStateResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetDashboardStateAsync() =>
			await Process(userId => _tutorialService.GetDashboardStateAsync(new PersonalSelectTaskUnitGrpcRequest {UserId = userId}), grpc => grpc.ToModel());

		[HttpPost("state")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<FinishUnitResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetFinishStateAsync([FromBody, Required] int unit)
		{
			if (EducationHelper.GetUnit(EducationTutorial.PersonalFinance, unit) == null)
				return StatusResponse.Error(ResponseCode.NotValidEducationRequestData);

			return await Process(userId => _tutorialService.GetFinishStateAsync(new GetFinishStateGrpcRequest {UserId = userId, Unit = unit}), grpc => grpc.ToModel());
		}

		private async ValueTask<IActionResult> Process<TGrpcResponse, TModelResponse>(
			Func<Guid?, ValueTask<TGrpcResponse>> grpcRequestFunc,
			Func<TGrpcResponse, TModelResponse> responseFunc)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			TGrpcResponse response = await grpcRequestFunc.Invoke(userId);

			return DataResponse<TModelResponse>.Ok(responseFunc.Invoke(response));
		}

		private async ValueTask<Guid?> GetUserIdAsync()
		{
			UserInfoResponse userInfoResponse = await _userInfoService.GetUserInfoByLoginAsync(new UserInfoAuthRequest
			{
				UserName = User.Identity?.Name
			});

			return userInfoResponse?.UserInfo?.UserId;
		}

		#region Unit1 (Your income)

		[HttpPost("unit1/text")]
		[OpenApiTag("Unit1")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TextAsync([FromBody] TaskTextRequest request) =>
			await Process(userId => _tutorialService.Unit1TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit1/test")]
		[OpenApiTag("Unit1")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TestAsync([FromBody] TaskTestRequest request) =>
			await Process(userId => _tutorialService.Unit1TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit1/case")]
		[OpenApiTag("Unit1")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1CaseAsync([FromBody] TaskCaseRequest request) =>
			await Process(userId => _tutorialService.Unit1CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit1/truefalse")]
		[OpenApiTag("Unit1")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TrueFalseAsync([FromBody] TaskTrueFalseRequest request) =>
			await Process(userId => _tutorialService.Unit1TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit1/game")]
		[OpenApiTag("Unit1")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1GameAsync([FromBody] TaskGameRequest request) =>
			await Process(userId => _tutorialService.Unit1GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit2 (Spending money secrets. Instruction. Exercise)

		[HttpPost("unit2/text")]
		[OpenApiTag("Unit2")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit2TextAsync([FromBody] TaskTextRequest request) =>
			await Process(userId => _tutorialService.Unit2TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit2/test")]
		[OpenApiTag("Unit2")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit2TestAsync([FromBody] TaskTestRequest request) =>
			await Process(userId => _tutorialService.Unit2TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit2/case")]
		[OpenApiTag("Unit2")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit2CaseAsync([FromBody] TaskCaseRequest request) =>
			await Process(userId => _tutorialService.Unit2CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit2/truefalse")]
		[OpenApiTag("Unit2")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit2TrueFalseAsync([FromBody] TaskTrueFalseRequest request) =>
			await Process(userId => _tutorialService.Unit2TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit2/game")]
		[OpenApiTag("Unit2")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit2GameAsync([FromBody] TaskGameRequest request) =>
			await Process(userId => _tutorialService.Unit2GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit3 (Hidden expenses and lost profits. Instruction. Exercise)

		[HttpPost("unit3/text")]
		[OpenApiTag("Unit3")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit3TextAsync([FromBody] TaskTextRequest request) =>
			await Process(userId => _tutorialService.Unit3TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit3/test")]
		[OpenApiTag("Unit3")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit3TestAsync([FromBody] TaskTestRequest request) =>
			await Process(userId => _tutorialService.Unit3TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit3/case")]
		[OpenApiTag("Unit3")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit3CaseAsync([FromBody] TaskCaseRequest request) =>
			await Process(userId => _tutorialService.Unit3CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit3/truefalse")]
		[OpenApiTag("Unit3")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit3TrueFalseAsync([FromBody] TaskTrueFalseRequest request) =>
			await Process(userId => _tutorialService.Unit3TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit3/game")]
		[OpenApiTag("Unit3")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit3GameAsync([FromBody] TaskGameRequest request) =>
			await Process(userId => _tutorialService.Unit3GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit4 (Salary - make sure that it is enough. Instruction. Exercise)

		[HttpPost("unit4/text")]
		[OpenApiTag("Unit4")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit4TextAsync([FromBody] TaskTextRequest request) =>
			await Process(userId => _tutorialService.Unit4TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit4/test")]
		[OpenApiTag("Unit4")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit4TestAsync([FromBody] TaskTestRequest request) =>
			await Process(userId => _tutorialService.Unit4TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit4/case")]
		[OpenApiTag("Unit4")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit4CaseAsync([FromBody] TaskCaseRequest request) =>
			await Process(userId => _tutorialService.Unit4CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit4/truefalse")]
		[OpenApiTag("Unit4")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit4TrueFalseAsync([FromBody] TaskTrueFalseRequest request) =>
			await Process(userId => _tutorialService.Unit4TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit4/game")]
		[OpenApiTag("Unit4")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit4GameAsync([FromBody] TaskGameRequest request) =>
			await Process(userId => _tutorialService.Unit4GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit5 (Budget planning in three steps. Modern tools. Instruction. Exercise)

		[HttpPost("unit5/text")]
		[OpenApiTag("Unit5")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit5TextAsync([FromBody] TaskTextRequest request) =>
			await Process(userId => _tutorialService.Unit5TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit5/test")]
		[OpenApiTag("Unit5")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit5TestAsync([FromBody] TaskTestRequest request) =>
			await Process(userId => _tutorialService.Unit5TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit5/case")]
		[OpenApiTag("Unit5")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit5CaseAsync([FromBody] TaskCaseRequest request) =>
			await Process(userId => _tutorialService.Unit5CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit5/truefalse")]
		[OpenApiTag("Unit5")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit5TrueFalseAsync([FromBody] TaskTrueFalseRequest request) =>
			await Process(userId => _tutorialService.Unit5TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("unit5/game")]
		[OpenApiTag("Unit5")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit5GameAsync([FromBody] TaskGameRequest request) =>
			await Process(userId => _tutorialService.Unit5GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion
	}
}