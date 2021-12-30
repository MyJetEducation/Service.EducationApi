using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.KeyValueModels
{
	public class KeyValueList
	{
		[Required]
		public KeyValueItem[] Items { get; set; }
	}
}