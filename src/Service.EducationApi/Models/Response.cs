using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;

namespace Service.EducationApi.Models
{
	public class Response<T>
	{
		public T Data { get; set; }
		public ResponseCode Status { get; set; }

		public static Response<T> Ok(T data) => new Response<T>
		{
			Data = data,
			Status = ResponseCode.Ok
		};

		public static ActionResult<T> Result(T data) => new OkObjectResult(Ok(data));

		public static Response<T> Error(ResponseCode code) => new Response<T>
		{
			Status = code
		};
	}
}