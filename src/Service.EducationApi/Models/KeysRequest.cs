using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class KeysRequest
	{
		[Required]
		public string[] Keys { get; set; }
	}
}