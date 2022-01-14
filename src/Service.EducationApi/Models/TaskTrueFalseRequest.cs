using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskTrueFalseRequest: TaskRequestBase
	{
		[Required]
		public TaskTrueFalse[] Answers { get; set; }
	}
}