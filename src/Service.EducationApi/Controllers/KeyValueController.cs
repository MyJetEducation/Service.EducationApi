using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;
using Service.EducationApi.Extensions;
using Service.EducationApi.Models;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.UserInfo.Crud.Grpc;
using CommonResponse = Service.KeyValue.Grpc.Models.CommonResponse;

namespace Service.EducationApi.Controllers
{
	[Authorize]
	[Route("/api/keyvalue/v1")]
	public class KeyValueController : BaseController
	{
		private readonly IKeyValueRepository _keyValueRepository;

		public KeyValueController(IUserInfoService userInfoService, IKeyValueRepository keyValueRepository)
			: base(userInfoService) => 
			_keyValueRepository = keyValueRepository;

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
				return StatusResponse.Error(ResponseCode.NoResponseData);

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

			return Result(response.IsSuccess);
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

			return Result(response.IsSuccess);
		}
	}
}