using System.ComponentModel.DataAnnotations;
using Service.KeyValue.Domain.Models;

namespace Service.EducationApi.Models
{
	public class KeyValueItem
	{
		public KeyValueItem(KeyValueModel model)
		{
			Key = model.Key;
			Value = model.Value;
		}

		public KeyValueItem()
		{
		}

		[Required]
		public string Key { get; set; }

		[Required]
		public string Value { get; set; }
	}
}