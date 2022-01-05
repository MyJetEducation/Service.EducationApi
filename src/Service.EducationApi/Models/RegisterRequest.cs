using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class RegisterRequest: LoginRequest
	{
		[Required]
		public string FullName { get; set; }
	}
}