using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.TaskModels
{
	public class TaskTrueFalse
	{
		[Required]
		public int Number { get; set; }

		[Required]
		public bool Value { get; set; }
	}
}