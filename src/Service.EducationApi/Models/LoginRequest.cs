using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class LoginRequest
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		public bool IsInvalid => string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password);
	}
}