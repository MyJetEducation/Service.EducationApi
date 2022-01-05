using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.EducationApi.Constants;
using Service.EducationApi.Mappers;
using Service.EducationApi.Models;
using Service.EducationApi.Models.TaskModels;
using Service.TutorialPersonal.Grpc;
using Service.TutorialPersonal.Grpc.Models;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/education/personal/v1")]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[OpenApiTag("EducationPersonal", Description = "personal finance tutorial")]
	public class EducationPersonalController : BaseController
	{
		private readonly ITutorialPersonalService _tutorialService;

		public EducationPersonalController(IUserInfoService userInfoService,
			ITutorialPersonalService tutorialService,
			ILogger<EducationPersonalController> logger)
			: base(userInfoService, logger) => _tutorialService = tutorialService;

		[HttpPost("/dashboard")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<PersonalStateResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetDashboardStateAsync() =>
			await Process(userId => _tutorialService.GetDashboardStateAsync(new PersonalSelectTaskUnitGrpcRequest {UserId = userId}), grpc => grpc.ToModel());

		[HttpPost("/state")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<FinishUnitResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetFinishStateAsync([FromBody, Required] int unit) =>
			await Process(userId => _tutorialService.GetFinishStateAsync(new GetFinishStateGrpcRequest {UserId = userId, Unit = unit}), grpc => grpc.ToModel());

		#region Unit1 (Your income)

		[HttpPost("/unit1/text")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit1TextAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("/unit1/test")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit1TestAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("/unit1/case")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit1CaseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("/unit1/truefalse")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit1TrueFalseAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		[HttpPost("/unit1/game")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TestScoreResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> Unit1GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit1GameAsync(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit2 (Spending money secrets)

		//[HttpPost("/unit2/text")]
		//public async ValueTask<IActionResult> Unit2TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit2Text(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit2/test")]
		//public async ValueTask<IActionResult> Unit2TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit2Test(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit2/case")]
		//public async ValueTask<IActionResult> Unit2CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit2Case(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit2/truefalse")]
		//public async ValueTask<IActionResult> Unit2TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit2TrueFalse(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit2/game")]
		//public async ValueTask<IActionResult> Unit2GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit2Game(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit3 (Hidden expenses and lost profits)

		//[HttpPost("/unit3/text")]
		//public async ValueTask<IActionResult> Unit3TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit3Text(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit3/test")]
		//public async ValueTask<IActionResult> Unit3TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit3Test(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit3/case")]
		//public async ValueTask<IActionResult> Unit3CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit3Case(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit3/truefalse")]
		//public async ValueTask<IActionResult> Unit3TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit3TrueFalse(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit3/game")]
		//public async ValueTask<IActionResult> Unit3GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit3Game(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit4 (Salary)

		//[HttpPost("/unit4/text")]
		//public async ValueTask<IActionResult> Unit4TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit4Text(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit4/test")]
		//public async ValueTask<IActionResult> Unit4TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit4Test(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit4/case")]
		//public async ValueTask<IActionResult> Unit4CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit4Case(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit4/truefalse")]
		//public async ValueTask<IActionResult> Unit4TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit4TrueFalse(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit4/game")]
		//public async ValueTask<IActionResult> Unit4GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit4Game(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		#region Unit5 (Budget planning in three steps)

		//[HttpPost("/unit5/text")]
		//public async ValueTask<IActionResult> Unit5TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit5Text(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit5/test")]
		//public async ValueTask<IActionResult> Unit5TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit5Test(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit5/case")]
		//public async ValueTask<IActionResult> Unit5CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit5Case(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit5/truefalse")]
		//public async ValueTask<IActionResult> Unit5TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit5TrueFalse(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		//[HttpPost("/unit5/game")]
		//public async ValueTask<IActionResult> Unit5GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit5Game(request.ToGrpcModel(userId)), grpc => grpc.ToModel());

		#endregion

		private async ValueTask<IActionResult> Process<TGrpcResponse, TModelResponse>(Func<Guid?, ValueTask<TGrpcResponse>> grpcRequestFunc, Func<TGrpcResponse, TModelResponse> responseFunc)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			TGrpcResponse response = await grpcRequestFunc.Invoke(userId);

			return DataResponse<TModelResponse>.Ok(responseFunc.Invoke(response));
		}
	}
}