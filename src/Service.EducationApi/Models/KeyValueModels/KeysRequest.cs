using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.KeyValueModels
{
	public class KeysRequest
	{
		[Required]
		public string[] Keys { get; set; }
	}
}