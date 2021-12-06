using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public interface ILoginRequestValidator
	{
		int? ValidateRequest(LoginRequest request);

		int? ValidateLogin(string value);

		bool ValidateRequired(LoginRequest request);
	}
}