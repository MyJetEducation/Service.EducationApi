using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskAnswer
	{
		[Required]
		public int Number { get; set; }

		[Required]
		public int[] Value { get; set; }
	}
}