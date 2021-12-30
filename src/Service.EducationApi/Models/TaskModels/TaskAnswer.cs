using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.TaskModels
{
	public class TaskAnswer
	{
		[Required]
		public int Number { get; set; }

		[Required]
		public int[] Value { get; set; }
	}
}