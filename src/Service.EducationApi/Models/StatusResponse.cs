using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Constants;

namespace Service.EducationApi.Models
{
	public class StatusResponse
	{
		public int Status { get; set; }

		public static IActionResult Error(int code = ResponseCode.Fail) => new OkObjectResult(
			new StatusResponse
			{
				Status = code
			});

		public static IActionResult Ok() => new OkObjectResult(
			new StatusResponse
			{
				Status = ResponseCode.Ok
			});
	}
}