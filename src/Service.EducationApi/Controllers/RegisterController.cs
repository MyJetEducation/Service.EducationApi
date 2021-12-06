using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;
using Service.EducationApi.Extensions;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Contracts;

namespace Service.EducationApi.Controllers
{
	[Route("/api/register/v1")]
	public class RegisterController : BaseController
	{
		private const int ActivationHashLength = 21;

		private readonly IUserInfoService _userInfoService;
		private readonly ILoginRequestValidator _loginRequestValidator;
		
		public RegisterController(IUserInfoService userInfoService, ILoginRequestValidator loginRequestValidator)
		{
			_userInfoService = userInfoService;
			_loginRequestValidator = loginRequestValidator;
		}

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> Register([FromBody] LoginRequest request)
		{
			int? validationResult = _loginRequestValidator.ValidateRequest(request);
			if (validationResult != null)
			{
				WaitFakeRequest();
				return StatusResponse.Error(validationResult.Value);
			}

			CommonResponse response = await _userInfoService.CreateUserInfoAsync(new UserInfoRegisterRequest
			{
				UserName = request.UserName,
				Password = request.Password
			});

			return Result(response.IsSuccess);
		}

		[HttpPost("confirm")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> ConfirmRegister([FromBody, Required] string hash)
		{
			if (hash.IsNullOrWhiteSpace() || hash.Length != ActivationHashLength)
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			CommonResponse response = await _userInfoService.ConfirmUserInfoAsync(new UserInfoConfirmRequest {Hash = hash});

			return Result(response.IsSuccess);
		}
	}
}