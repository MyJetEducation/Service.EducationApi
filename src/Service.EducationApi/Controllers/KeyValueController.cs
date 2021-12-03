using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.EducationApi.Constants;
using Service.EducationApi.Extensions;
using Service.EducationApi.Models;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using Service.UserInfo.Crud.Grpc.Contracts;
using CommonResponse = Service.KeyValue.Grpc.Models.CommonResponse;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("/api/keyvalue/v1")]
	public class KeyValueController : ControllerBase
	{
		private readonly IUserInfoService _userInfoService;
		private readonly IKeyValueRepository _keyValueRepository;
		private readonly ILogger<KeyValueController> _logger;

		public KeyValueController(IUserInfoService userInfoService, IKeyValueRepository keyValueRepository, ILogger<KeyValueController> logger)
		{
			_userInfoService = userInfoService;
			_keyValueRepository = keyValueRepository;
			_logger = logger;
		}

		[HttpPost("get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> GetAsync([FromBody] KeysRequest keysRequest)
		{
			string[] keys = keysRequest?.Keys;
			if (keys.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			ItemsResponse itemsResponse = await _keyValueRepository.Get(new ItemsGetRequest
			{
				UserId = userId,
				Keys = keys
			});

			KeyValueModel[] items = itemsResponse?.Items;
			if (items == null)
				return StatusResponse.Error(ResponseCode.NoData);

			return DataResponse<KeyValueList>.Ok(new KeyValueList
			{
				Items = items.Select(keyValueModel => new KeyValueItem(keyValueModel)).ToArray()
			});
		}

		[HttpPost("put")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> PutAsync([FromBody] KeyValueList keyValueList)
		{
			KeyValueItem[] items = keyValueList?.Items;
			if (items.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonResponse response = await _keyValueRepository.Put(new ItemsPutRequest
			{
				UserId = userId,
				Items = items?.Select(item => new KeyValueModel {Key = item.Key, Value = item.Value}).ToArray()
			});

			return HandleCommonResponse(response);
		}

		[HttpPost("delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async ValueTask<IActionResult> DeleteAsync([FromBody] KeysRequest keysRequest)
		{
			string[] keys = keysRequest?.Keys;
			if (keys.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonResponse response = await _keyValueRepository.Delete(new ItemsDeleteRequest
			{
				UserId = userId,
				Keys = keys
			});

			return HandleCommonResponse(response);
		}

		private static IActionResult HandleCommonResponse(CommonResponse response) => response?.IsSuccess == true
			? StatusResponse.Ok()
			: StatusResponse.Error();

		private async ValueTask<Guid?> GetUserIdAsync()
		{
			string userName = User.Identity?.Name;

			_logger.LogDebug("UserName from identity is {userName}", userName);

			UserIdResponse userIdResponse = await _userInfoService.GetUserIdAsync(new UserInfoLoginRequest
			{
				UserName = userName
			});

			return userIdResponse?.UserId;
		}
	}
}