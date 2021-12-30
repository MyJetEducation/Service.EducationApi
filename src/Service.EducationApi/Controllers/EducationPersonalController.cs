using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Mappers;
using Service.EducationApi.Models.TaskModels;
using Service.TutorialPersonal.Grpc;
using Service.TutorialPersonal.Grpc.Models;
using Service.TutorialPersonal.Grpc.Models.State;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/education/personal/v1")]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public class EducationPersonalController : BaseController
	{
		private readonly ITutorialPersonalService _tutorialService;

		public EducationPersonalController(IUserInfoService userInfoService, ITutorialPersonalService tutorialService) : base(userInfoService) => _tutorialService = tutorialService;

		[HttpPost("/dashboard")]
		public async ValueTask<PersonalStateResponse> GetDashboardStateAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return new PersonalStateResponse {Available = false};

			PersonalStateGrpcResponse response = await _tutorialService.GetDashboardStateAsync(new PersonalSelectTaskUnitGrpcRequest {UserId = userId});

			return response.ToModel();
		}

		#region Unit1 (Your income)

		[HttpPost("/unit1/text")]
		public async ValueTask<TestScoreResponse> Unit1TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit1TextAsync(request.ToGrpcModel(userId)));

		[HttpPost("/unit1/test")]
		public async ValueTask<TestScoreResponse> Unit1TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit1TestAsync(request.ToGrpcModel(userId)));

		[HttpPost("/unit1/case")]
		public async ValueTask<TestScoreResponse> Unit1CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit1CaseAsync(request.ToGrpcModel(userId)));

		[HttpPost("/unit1/truefalse")]
		public async ValueTask<TestScoreResponse> Unit1TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit1TrueFalseAsync(request.ToGrpcModel(userId)));

		[HttpPost("/unit1/game")]
		public async ValueTask<TestScoreResponse> Unit1GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit1GameAsync(request.ToGrpcModel(userId)));

		#endregion

		#region Unit2 (Spending money secrets)

		//[HttpPost("/unit2/text")]
		//public async ValueTask<IActionResult> Unit2TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit2Text(request.ToGrpcModel(userId)));

		//[HttpPost("/unit2/test")]
		//public async ValueTask<IActionResult> Unit2TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit2Test(request.ToGrpcModel(userId)));

		//[HttpPost("/unit2/case")]
		//public async ValueTask<IActionResult> Unit2CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit2Case(request.ToGrpcModel(userId)));

		//[HttpPost("/unit2/truefalse")]
		//public async ValueTask<IActionResult> Unit2TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit2TrueFalse(request.ToGrpcModel(userId)));

		//[HttpPost("/unit2/game")]
		//public async ValueTask<IActionResult> Unit2GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit2Game(request.ToGrpcModel(userId)));

		#endregion

		#region Unit3 (Hidden expenses and lost profits)

		//[HttpPost("/unit3/text")]
		//public async ValueTask<IActionResult> Unit3TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit3Text(request.ToGrpcModel(userId)));

		//[HttpPost("/unit3/test")]
		//public async ValueTask<IActionResult> Unit3TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit3Test(request.ToGrpcModel(userId)));

		//[HttpPost("/unit3/case")]
		//public async ValueTask<IActionResult> Unit3CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit3Case(request.ToGrpcModel(userId)));

		//[HttpPost("/unit3/truefalse")]
		//public async ValueTask<IActionResult> Unit3TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit3TrueFalse(request.ToGrpcModel(userId)));

		//[HttpPost("/unit3/game")]
		//public async ValueTask<IActionResult> Unit3GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit3Game(request.ToGrpcModel(userId)));

		#endregion

		#region Unit4 (Salary)

		//[HttpPost("/unit4/text")]
		//public async ValueTask<IActionResult> Unit4TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit4Text(request.ToGrpcModel(userId)));

		//[HttpPost("/unit4/test")]
		//public async ValueTask<IActionResult> Unit4TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit4Test(request.ToGrpcModel(userId)));

		//[HttpPost("/unit4/case")]
		//public async ValueTask<IActionResult> Unit4CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit4Case(request.ToGrpcModel(userId)));

		//[HttpPost("/unit4/truefalse")]
		//public async ValueTask<IActionResult> Unit4TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit4TrueFalse(request.ToGrpcModel(userId)));

		//[HttpPost("/unit4/game")]
		//public async ValueTask<IActionResult> Unit4GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit4Game(request.ToGrpcModel(userId)));

		#endregion

		#region Unit5 (Budget planning in three steps)

		//[HttpPost("/unit5/text")]
		//public async ValueTask<IActionResult> Unit5TextAsync([FromBody] TaskTextRequest request) => await Process(userId => _tutorialService.Unit5Text(request.ToGrpcModel(userId)));

		//[HttpPost("/unit5/test")]
		//public async ValueTask<IActionResult> Unit5TestAsync([FromBody] TaskTestRequest request) => await Process(userId => _tutorialService.Unit5Test(request.ToGrpcModel(userId)));

		//[HttpPost("/unit5/case")]
		//public async ValueTask<IActionResult> Unit5CaseAsync([FromBody] TaskCaseRequest request) => await Process(userId => _tutorialService.Unit5Case(request.ToGrpcModel(userId)));

		//[HttpPost("/unit5/truefalse")]
		//public async ValueTask<IActionResult> Unit5TrueFalseAsync([FromBody] TaskTrueFalseRequest request) => await Process(userId => _tutorialService.Unit5TrueFalse(request.ToGrpcModel(userId)));

		//[HttpPost("/unit5/game")]
		//public async ValueTask<IActionResult> Unit5GameAsync([FromBody] TaskGameRequest request) => await Process(userId => _tutorialService.Unit5Game(request.ToGrpcModel(userId)));

		#endregion

		private async ValueTask<TestScoreResponse> Process(Func<Guid?, ValueTask<TestScoreGrpcResponse>> action)
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return new TestScoreResponse {IsSuccess = false};

			TestScoreGrpcResponse response = await action.Invoke(userId);

			return response.ToModel();
		}
	}
}