using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskTrueFalseRequest: TaskRequestBase
	{
		[Required]
		public TaskTrueFalse[] Answers { get; set; }
	}
}