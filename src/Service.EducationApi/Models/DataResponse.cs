using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;

namespace Service.EducationApi.Models
{
	public class DataResponse<T> : StatusResponse
	{
		public T Data { get; set; }

		public static IActionResult Ok(T data) => new OkObjectResult(
			new DataResponse<T>
			{
				Data = data,
				Status = data == null ? ResponseCode.Fail : ResponseCode.Ok
			});
	}
}