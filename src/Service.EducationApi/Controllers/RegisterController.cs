using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;
using Service.EducationApi.Extensions;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Service.PasswordRecovery.Grpc;
using Service.PasswordRecovery.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Models;
using CommonGrpcResponse = Service.UserInfo.Crud.Grpc.Models.CommonGrpcResponse;

namespace Service.EducationApi.Controllers
{
	[Route("/api/register/v1")]
	public class RegisterController : BaseController
	{
		private readonly IUserInfoService _userInfoService;
		private readonly ILoginRequestValidator _loginRequestValidator;
		private readonly IPasswordRecoveryService _passwordRecoveryService;

		public RegisterController(IUserInfoService userInfoService, 
			ILoginRequestValidator loginRequestValidator, 
			IPasswordRecoveryService passwordRecoveryService) : base(userInfoService)
		{
			_userInfoService = userInfoService;
			_loginRequestValidator = loginRequestValidator;
			_passwordRecoveryService = passwordRecoveryService;
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

			Guid? userId = await GetUserIdAsync(request.UserName);
			if (userId != null)
				return StatusResponse.Error(ResponseCode.UserAlreadyExists);

			CommonGrpcResponse response = await _userInfoService.CreateUserInfoAsync(new UserInfoRegisterRequest
			{
				UserName = request.UserName,
				Password = request.Password
			});

			return Result(response?.IsSuccess);
		}

		[HttpPost("confirm")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> ConfirmRegister([FromBody, Required] string hash)
		{
			if (hash.IsNullOrWhiteSpace())
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			CommonGrpcResponse response = await _userInfoService.ConfirmUserInfoAsync(new UserInfoConfirmRequest {Hash = hash});

			return Result(response?.IsSuccess);
		}

		[HttpPost("recovery")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> PasswordRecovery([FromBody, Required] string email)
		{
			if (email.IsNullOrWhiteSpace())
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			PasswordRecovery.Grpc.Models.CommonGrpcResponse response = await _passwordRecoveryService.Recovery(new RecoveryPasswordGrpcRequest { Email = email });

			return Result(response?.IsSuccess);
		}

		[HttpPost("change")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> ChangePassword([FromBody, Required] ChangePasswordRequest request)
		{
			string password = request.Password;
			string hash = request.Hash;

			if (password.IsNullOrWhiteSpace() || hash.IsNullOrWhiteSpace())
			{
				WaitFakeRequest();
				return StatusResponse.Error(ResponseCode.NoRequestData);
			}

			PasswordRecovery.Grpc.Models.CommonGrpcResponse response = await _passwordRecoveryService.Change(new ChangePasswordGrpcRequest { Password = password, Hash = hash });

			return Result(response?.IsSuccess);
		}
	}
}