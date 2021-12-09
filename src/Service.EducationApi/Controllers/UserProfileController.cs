using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserProfile.Grpc;
using Service.UserProfile.Grpc.Models;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("api/userprofile/v1")]
	public class UserProfileController : BaseController
	{
		private readonly IUserProfileService _userProfileService;

		public UserProfileController(IUserInfoService userInfoService, IUserProfileService userProfileService) : base(userInfoService)
		{
			_userProfileService = userProfileService;
		}

		[HttpPost("questions")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> GetQuestions()
		{
			QuestionGrpcResponse questions = await _userProfileService.GetQuestions();

			QuestionDataGrpcModel[] questionsData = questions?.Data;

			return questionsData == null
				? StatusResponse.Error(ResponseCode.NoResponseData)
				: DataResponse<QuestionDataGrpcModel[]>.Ok(questionsData);
		}
	}
}