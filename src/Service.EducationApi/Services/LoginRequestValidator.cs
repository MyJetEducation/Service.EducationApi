using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Service.Core.Domain.Extensions;
using Service.EducationApi.Constants;
using Service.EducationApi.Models;

namespace Service.EducationApi.Services
{
	public class LoginRequestValidator : ILoginRequestValidator
	{
		private static readonly Regex PasswordRegex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,31}$", RegexOptions.Compiled);

		public int? ValidateRequest(LoginRequest request) => ValidateLogin(request.UserName) ?? ValidatePassword(request.Password);

		public int? ValidateLogin(string value)
		{
			if (value.IsNullOrWhiteSpace())
				return ResponseCode.NoRequestData;

			if (!new EmailAddressAttribute().IsValid(value))
				return ResponseCode.NotValidEmail;

			return null;
		}

		public int? ValidatePassword(string value)
		{
			if (value.IsNullOrWhiteSpace())
				return ResponseCode.NoRequestData;

			if (!PasswordRegex.IsMatch(value))
				return ResponseCode.NotValidPassword;

			return null;
		}

		public bool ValidateRequired(LoginRequest request) => request.UserName.IsNullOrWhiteSpace() || request.Password.IsNullOrWhiteSpace();
	}
}