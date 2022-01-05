using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.Core.Domain.Extensions;
using Service.Core.Grpc.Models;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.EducationApi.Services;
using Service.PasswordRecovery.Grpc;
using Service.PasswordRecovery.Grpc.Models;
using Service.Registration.Grpc;
using Service.Registration.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using SimpleTrading.ClientApi.Utils;

namespace Service.EducationApi.Controllers
{
	[Route("/api/register/v1")]
	[OpenApiTag("Register", Description = "user registration")]
	public class RegisterController : BaseController
	{
		private readonly ILoginRequestValidator _loginRequestValidator;
		private readonly IPasswordRecoveryService _passwordRecoveryService;
		private readonly IRegistrationService _registrationService;
		private readonly ITokenService _tokenService;

		public RegisterController(IUserInfoService userInfoService,
			ILoginRequestValidator loginRequestValidator,
			IPasswordRecoveryService passwordRecoveryService,
			IRegistrationService registrationService,
			ITokenService tokenService,
			ILogger<RegisterController> logger) : base(userInfoService, logger)
		{
			_loginRequestValidator = loginRequestValidator;
			_passwordRecoveryService = passwordRecoveryService;
			_registrationService = registrationService;
			_tokenService = tokenService;
		}

		[HttpPost("create")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(StatusResponse), Description = "Ok")]
		public async ValueTask<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
		{
			int? validationResult = _loginRequestValidator.ValidateRegisterRequest(request);
			if (validationResult != null)
			{
				WaitFakeRequest();
				return StatusResponse.Error(validationResult.Value);
			}

			Guid? userId = await GetUserIdAsync(request.UserName);
			if (userId != null)
				return StatusResponse.Error(ResponseCode.UserAlreadyExists);

			CommonGrpcResponse response = await _registrationService.RegistrationAsync(new RegistrationGrpcRequest
			{
				UserName = request.UserName,
				Password = request.Password,
				FullName = request.FullName
			});

			return Result(response?.IsSuccess);
		}

		[HttpPost("confirm")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<TokenInfo>), Description = "Ok")]
		[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
		public async ValueTask<IActionResult> ConfirmRegisterAsync([FromBody, Required] string hash)
		{
			ConfirmRegistrationGrpcResponse response = await _registrationService.ConfirmRegistrationAsync(new ConfirmRegistrationGrpcRequest {Hash = hash});

			string userName = response?.Email;
			if (userName.IsNullOrEmpty())
				return StatusResponse.Error();

			TokenInfo tokenInfo = await _tokenService.GenerateTokensAsync(userName, HttpContext.GetIp());
			return tokenInfo != null
				? DataResponse<TokenInfo>.Ok(tokenInfo)
				: Unauthorized();
		}

		[HttpPost("recovery")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(StatusResponse), Description = "Ok")]
		public async ValueTask<IActionResult> PasswordRecoveryAsync([FromBody, Required] string email)
		{
			CommonGrpcResponse response = await _passwordRecoveryService.Recovery(new RecoveryPasswordGrpcRequest {Email = email});

			return Result(response?.IsSuccess);
		}

		[HttpPost("change")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(StatusResponse), Description = "Ok")]
		public async ValueTask<IActionResult> ChangePasswordAsync([FromBody, Required] ChangePasswordRequest request)
		{
			string hash = request.Hash;
			string password = request.Password;

			int? validationResult = _loginRequestValidator.ValidatePassword(password);
			if (validationResult != null)
			{
				WaitFakeRequest();
				return StatusResponse.Error(validationResult.Value);
			}

			CommonGrpcResponse response = await _passwordRecoveryService.Change(new ChangePasswordGrpcRequest {Password = password, Hash = hash});

			return Result(response?.IsSuccess);
		}
	}
}