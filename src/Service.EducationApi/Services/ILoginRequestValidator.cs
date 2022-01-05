using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public interface ILoginRequestValidator
	{
		int? ValidateLoginRequest(LoginRequest request);

		int? ValidateRegisterRequest(RegisterRequest request);

		int? ValidatePassword(string value);
	}
}