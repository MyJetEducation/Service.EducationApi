using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Service.Core.Domain.Extensions;
using Service.Core.Grpc.Models;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;
using Service.EducationApi.Models.KeyValueModels;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/keyvalue/v1")]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[OpenApiTag("KeyValue", Description = "key-value common storage")]
	public class KeyValueController : BaseController
	{
		private readonly IKeyValueService _keyValueService;

		public KeyValueController(IUserInfoService userInfoService,
			IKeyValueService keyValueService,
			ILogger<KeyValueController> logger)
			: base(userInfoService, logger) => _keyValueService = keyValueService;

		[HttpPost("get")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<KeyValueList>), Description = "Ok")]
		public async ValueTask<IActionResult> GetAsync([FromBody] KeysRequest keysRequest)
		{
			string[] keys = keysRequest?.Keys;
			if (keys.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			ItemsGrpcResponse itemsResponse = await _keyValueService.Get(new ItemsGetGrpcRequest
			{
				UserId = userId,
				Keys = keys
			});

			KeyValueGrpcModel[] items = itemsResponse?.Items;
			if (items == null)
				return StatusResponse.Error(ResponseCode.NoResponseData);

			return DataResponse<KeyValueList>.Ok(new KeyValueList
			{
				Items = items.Select(keyValueModel => new KeyValueItem(keyValueModel)).ToArray()
			});
		}

		[HttpPost("put")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(StatusResponse), Description = "Ok")]
		public async ValueTask<IActionResult> PutAsync([FromBody] KeyValueList keyValueList)
		{
			KeyValueItem[] items = keyValueList?.Items;
			if (items.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _keyValueService.Put(new ItemsPutGrpcRequest
			{
				UserId = userId,
				Items = items?.Select(item => new KeyValueGrpcModel {Key = item.Key, Value = item.Value}).ToArray()
			});

			return Result(response?.IsSuccess);
		}

		[HttpPost("delete")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(StatusResponse), Description = "Ok")]
		public async ValueTask<IActionResult> DeleteAsync([FromBody] KeysRequest keysRequest)
		{
			string[] keys = keysRequest?.Keys;
			if (keys.IsNullOrEmpty())
				return StatusResponse.Error(ResponseCode.NoRequestData);

			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			CommonGrpcResponse response = await _keyValueService.Delete(new ItemsDeleteGrpcRequest
			{
				UserId = userId,
				Keys = keys
			});

			return Result(response?.IsSuccess);
		}

		[HttpPost("keys")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(DataResponse<KeysResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetKeysAsync()
		{
			Guid? userId = await GetUserIdAsync();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			KeysGrpcResponse keysResponse = await _keyValueService.GetKeys(new GetKeysGrpcRequest
			{
				UserId = userId,
			});

			string[] items = keysResponse?.Keys;
			if (items == null)
				return StatusResponse.Error(ResponseCode.NoResponseData);

			return DataResponse<KeysResponse>.Ok(new KeysResponse
			{
				Keys = items
			});
		}
	}
}