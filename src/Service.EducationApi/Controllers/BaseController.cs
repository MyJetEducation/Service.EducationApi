using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Service.EducationApi.Models;

namespace Service.EducationApi.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected static void WaitFakeRequest() => Thread.Sleep(200);

		protected static IActionResult Result(bool isSuccess) => isSuccess ? StatusResponse.Ok() : StatusResponse.Error();
	}
}